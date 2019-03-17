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
}
[Serializable]
public class StaticDataReference<TData, TReference, TDirectory>
    where TData:Object 
    where TReference:StaticDataReference<TData, TReference, TDirectory>, new()
    where TDirectory:StaticDataReferenceDirectory<TData, TReference, TDirectory>, new()
{
    [SerializeField]
    private string _id;
    public string ID
    {
        get { return _id;}
        set { _id = value; }
    }

    public TData Data
    {
        get
        {
            if (string.IsNullOrEmpty(ID))
                return null;
                
            return Game.Instance.GetStaticDataPovider<TDirectory>().FindData(ID); 
        }
    }
}