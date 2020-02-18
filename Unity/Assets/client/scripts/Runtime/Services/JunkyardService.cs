using System;
using System.IO;
using PandeaGames.Services;
using Polenter.Serialization;
using UnityEngine;

public class JunkyardService : AbstractService<JunkyardService>
{
    private SharpSerializer _serializer;
    
    public static string SAVE_FOLDER_PATH
    {
        get { return string.Format("{0}/{1}", Application.persistentDataPath, "Junkyards"); }
    }
    
    public JunkyardService()
    {
        _serializer = new SharpSerializer();
    }

    private static string GetFilePath(string dataName)
    {
        return string.Format("{0}/{1}.data", SAVE_FOLDER_PATH, dataName);
    }

    public bool DeleteJunkyardData(JunkyardData junkyardData)
    {
        string filepath = GetFilePath(junkyardData.name);
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
            Debug.Log("[JunkyardService.DeleteJunkyardData] Deleted JunkyardData at path "+ filepath);
        }
        else
        {
            Debug.Log("[JunkyardService.DeleteJunkyardData] Attempted to delete JunkyardData at path "+ filepath);
            return false;
        }

        return true;
    }

    public Junkyard GetJunkyard(JunkyardData junkyardData)
    {
        SerializedJunkyard serializedJunkyard = null;
        string filepath = GetFilePath(junkyardData.name);
        
        try
        {
            if (!File.Exists(filepath))
            {
                throw new Exception("There is no junkyard data at "+ filepath);
            }
            
            Debug.Log("Loading SerializedJunkyard from "+ filepath);
            
            FileStream fileStream = File.Open(filepath, FileMode.Open);

            using (var stream = fileStream)
            {
                serializedJunkyard = _serializer.Deserialize(stream) as SerializedJunkyard;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            serializedJunkyard = junkyardData.Generate();
        }

        return new Junkyard(junkyardData, serializedJunkyard);
    }

    public void SaveJunkyard(Junkyard junkyard)
    {
        string filepath = GetFilePath(junkyard.ID);
        string directory = Path.GetDirectoryName(filepath);
        
        try
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            FileStream fileStream = File.Open(filepath, FileMode.OpenOrCreate);

            using (var reader = fileStream)
            {
                _serializer.Serialize(junkyard.serializedJunkyard, fileStream);
            }

            Debug.Log("User Data saved at "+ filepath + ": " + junkyard.ID);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save user: " + e);
        }
    }
}
