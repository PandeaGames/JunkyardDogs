using UnityEngine;
using System.Collections.Generic;
using System;
using Polenter.Serialization;
using System.IO;

public static class UserServiceUtils
{
    public const string USER_DATA_KEY = "user_data";
    public const string USER_DATA_BACKUP_KEY = "user_data_backup";

    public static void ClearUserData()
    {
        PlayerPrefs.DeleteKey(USER_DATA_KEY);
    }

    public static T Load<T>() where T : User, new()
    {
        return Load<T>(new SharpSerializer());
    }

    public static T Load<T>(SharpSerializer serializer) where T : User, new()
    {
        T user = null;

        try
        {
            if (!PlayerPrefs.HasKey(UserServiceUtils.USER_DATA_KEY))
            {
                throw new Exception("There is no user data saved");
            }

            string savedData = PlayerPrefs.GetString(UserServiceUtils.USER_DATA_KEY);

            using (var stream = GenerateStreamFromString(savedData))
            {
                user = serializer.Deserialize(stream) as T;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("There was an error loading user data: \n" + e);
            user = new T();
            user.UID = SystemInfo.deviceUniqueIdentifier;
        }

        return user;
    }

    public static void Save(User user)
    {
        Save(user, new SharpSerializer());
    }

    public static void Save(User user, SharpSerializer serializer)
    {
        try
        {
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(user, stream);

            string serializedString;

            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                serializedString = reader.ReadToEnd();
            }

            PlayerPrefs.SetString(UserServiceUtils.USER_DATA_KEY, serializedString);
            Debug.Log("User Data saved: " + user.UID);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save user: " + e);
        }
    }

    private static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}

[Serializable]
public abstract class UserService<T> : Service where T:User, new()
{
    [SerializeField]
    private T _user;

    private SharpSerializer _serializer;

    public T User { get { return _user; } }

    public override void StartService(ServiceManager serviceManager)
    {
        base.StartService(serviceManager);
        _serializer = new SharpSerializer();
        _user = Load();
    }

    public T Load()
    {
        if(_user != null)
        {
            return _user;
        }

        return UserServiceUtils.Load<T>(_serializer);
    }

    public void Save()
    {
        Save(_user);
    }

    public void Save(T user)
    {
        UserServiceUtils.Save(user, _serializer);
    }

    protected virtual string SerializeUser(User user)
    {
        return JsonUtility.ToJson(user); 
    }

    public void ClearUserData()
    {
        UserServiceUtils.ClearUserData();
        Debug.Log("User Data cleared.");
    }
}