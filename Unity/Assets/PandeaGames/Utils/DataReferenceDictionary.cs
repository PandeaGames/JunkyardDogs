using System;
using PandeaGames.Data.Static;
using Object = UnityEngine.Object;

namespace PandeaGames.Utils
{
    [Serializable]
    public class DataReferenceDictionaryKVP<TReference,TValue> : KeyValuePair<TReference, TValue>
    {
        
    }
    
    [Serializable]
    public class DataReferenceDictionary<TReference, TData, TDataBase, TDirectory, TValue, TKvP> : SerializableDictionary<TReference, TValue, TKvP> 
        where TDataBase:Object
        where TData:TDataBase
        where TKvP:KeyValuePair<TReference, TValue>
        where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
        
    {
        
    }
}