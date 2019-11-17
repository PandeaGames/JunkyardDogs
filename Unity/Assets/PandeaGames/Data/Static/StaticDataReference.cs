using System;
using PandeaGames;
using UnityEngine;
using PandeaGames.Data.Static;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Object = UnityEngine.Object;

namespace PandeaGames.Data.Static
{
   
}
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public class StaticDataReferenceAttribute : PropertyAttribute
{
    public string[] IDs;
    public StaticDataReferenceAttribute(string path)
    {
            #if UNITY_EDITOR
        Object requestResult = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
        IStaticDataDirectorySource source = requestResult as IStaticDataDirectorySource;
        IDs = source.GetIDs();
#endif
    }
    
    public StaticDataReferenceAttribute(string path, Type filterType)
    {
#if UNITY_EDITOR
        Object requestResult = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
        IStaticDataDirectorySource source = requestResult as IStaticDataDirectorySource;
        IDs = source.GetIDs(filterType);
#endif
    }
}

public interface IStaticData
{
    string ID { get; }
}

[Serializable]
public class StaticDataReference<TDataBase, TData, TReference, TDirectory>
    where TDataBase:IStaticData
    where TData:TDataBase
    where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
    where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
{
    [SerializeField]
    private string _id;
    public string ID
    {
        get { return _id;}
        set { _id = value; }
    }

    public override string ToString()
    {
        return ID;
    }

    public TData Data
    {
        get
        {
            if (string.IsNullOrEmpty(ID))
            {
                Debug.LogWarningFormat("Failed to load Data of type {0} because the ID is empty.", typeof(TData));
                return default(TData);
            }
 
            return Game.Instance.GetStaticDataPovider<TDirectory>().FindData(ID); 
        }
    }

    public bool Equals(StaticDataReference<TDataBase, TData, TReference, TDirectory> other)
    {
        return other != null && other.ID == ID;
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }
}