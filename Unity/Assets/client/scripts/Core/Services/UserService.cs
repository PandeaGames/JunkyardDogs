using UnityEngine;
using System.Collections.Generic;
using System;
using Polenter.Serialization;
using System.IO;

[Serializable]
public abstract class UserService<T> : Service where T:User, new()
{
    private const string USER_DATA_KEY = "user_data";
    private const string USER_DATA_BACKUP_KEY = "user_data_backup";

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
        T user = null;
        try
        {
            if (!PlayerPrefs.HasKey(USER_DATA_KEY))
            {
                throw new Exception("There is no user data saved");
            }

            string savedData = PlayerPrefs.GetString(USER_DATA_KEY);

            using (var stream = GenerateStreamFromString(savedData))
            {
                user = _serializer.Deserialize(stream) as T;
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

    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public void Save()
    {
        Save(_user);
    }

    public void Save(User user)
    {
        try
        {
            MemoryStream stream = new MemoryStream();
            _serializer.Serialize(user, stream);

            string serializedString;

            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                serializedString = reader.ReadToEnd();
            }

            PlayerPrefs.SetString(USER_DATA_KEY, serializedString);
            Debug.Log("User Data saved: " + user.UID);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save user: " + e);
        }
    }

    protected virtual string SerializeUser(User user)
    {
        return JsonUtility.ToJson(user); 
    }

    public void ClearUserData()
    {
        PlayerPrefs.SetString(USER_DATA_KEY, "");
        Debug.Log("User Data cleared.");
    }
}