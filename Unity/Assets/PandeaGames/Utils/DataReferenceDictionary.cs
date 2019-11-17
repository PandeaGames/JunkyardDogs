using System;
using PandeaGames.Data.Static;

namespace PandeaGames.Utils
{
    [Serializable]
    public class DataReferenceDictionaryKVP<TReference,TValue> : KeyValuePair<TReference, TValue>
    {
        
    }
    
    [Serializable]
    public class DataReferenceDictionary<TReference, TData, TDataBase, TDirectory, TValue, TKvP> : SerializableDictionary<TReference, TValue, TKvP>, ISerializableDictionary<TData, TValue>
        where TDataBase:IStaticData
        where TData:TDataBase
        where TKvP:KeyValuePair<TReference, TValue>, new()
        where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
    {
        public virtual TValue GetValue(TData key)
        {
            return GetValueByObj(key);
        }

        public void SetValue(TData key, TValue value)
        {
            AddValue(key, value);
        }

        public virtual void AddValue(TData key, TValue value)
        {
            TReference reference = new TReference();
            reference.ID = key.ID;
            base.AddValue(reference, value);
        }

        public virtual bool Contains(TData data)
        {
            return base.ContainsObj(data);
        }

        public virtual void Remove(TData key)
        {
            base.RemoveObj(key);
        }
    }
}