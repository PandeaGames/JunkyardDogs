using UnityEngine;
using System.Collections.Generic;
using System;
using Polenter.Serialization;
using System.IO;
using PandeaGames.Services;

public static class UserServiceUtils
{
    public const string USER_DATA_KEY = "user_data";
    public const string USER_DATA_BACKUP_KEY = "user_data_backup";

    public static string SAVE_FILE_PATH
    {
        get { return string.Format("{0}/{1}", Application.persistentDataPath, "UserData.data"); }
    }

    public static void ClearUserData()
    {
        ClearUserData(string.Empty);
    }

    public static void ClearUserData(string prefix)
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            File.Delete(SAVE_FILE_PATH);
        }
    }

    public static T Load<T>(string prefix) where T : User, new()
    {
        return Load<T>(new SharpSerializer(), prefix);
    }

    public static T Load<T>() where T : User, new()
    {
        return Load<T>(new SharpSerializer());
    }

    public static T Load<T>(SharpSerializer serializer) where T : User, new()
    {
        return Load<T>(serializer, string.Empty);
    }

    public static T Load<T>(SharpSerializer serializer, string prefix) where T : User, new()
    {
        T user = null;

        try
        {
            if (!File.Exists(SAVE_FILE_PATH))
            {
                throw new Exception("There is no user data saved at "+ SAVE_FILE_PATH);
            }
            
            Debug.Log("Loading user from "+ SAVE_FILE_PATH);
            
            FileStream fileStream = File.Open(SAVE_FILE_PATH, FileMode.Open);

            using (var stream = fileStream)
            {
                user = serializer.Deserialize(stream) as T;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("There was an error loading user data at "+ string.Format("{0}{1}", prefix, UserServiceUtils.USER_DATA_KEY) + ": \n" + e);
            user = new T();
            user.UID = SystemInfo.deviceUniqueIdentifier;
        }

        return user;
    }

    public static void Save(User user, string prefix)
    {
        Save(user, new SharpSerializer(), prefix);
    }

    public static void Save(User user)
    {
        Save(user, string.Empty);
    }

    public static void Save(User user, SharpSerializer serializer, string prefix)
    {   
        try
        {
            FileStream fileStream = File.Open(SAVE_FILE_PATH, FileMode.OpenOrCreate);

            using (var reader = fileStream)
            {
                serializer.Serialize(user, fileStream);
            }

            Debug.Log("User Data saved at "+ SAVE_FILE_PATH + ": " + user.UID);
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
public abstract class AbstractUserService<T, TService> : AbstractService<TService> where T:User, new() where TService:AbstractUserService<T, TService>, new()
{   
    [SerializeField]
    protected string _userDataStorageKeyPrefix;

    private T _user;

    private SharpSerializer _serializer;

    public T User { get { return _user; } }

    public AbstractUserService()
    {
        _serializer = new SharpSerializer();
        _user = Load();
    }

    public virtual T Load()
    {
        if(_user != null)
        {
            return _user;
        }
        
        _user = UserServiceUtils.Load<T>(_serializer, _userDataStorageKeyPrefix);

        return _user;
    }

    public virtual void Save()
    {
        Save(_user);
    }

    public virtual void Save(T user)
    {
        UserServiceUtils.Save(user, _serializer, _userDataStorageKeyPrefix);
    }

    public virtual void ClearUserData()
    {
        UserServiceUtils.ClearUserData(_userDataStorageKeyPrefix);
        Debug.Log("User Data cleared at " + string.Format("{0}{1}", _userDataStorageKeyPrefix, UserServiceUtils.USER_DATA_KEY));
    }
}