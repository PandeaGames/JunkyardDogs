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
        public virtual TValue this[TData key]
        {
            get { return this[key as object]; }
            set
            {
                
            }
        }

        public virtual void Add(TData key, TValue value)
        {
            TReference reference = new TReference();
            reference.ID = key.ID;
            base.Add(reference, value);
        }

        public virtual bool Contains(TData data)
        {
            return base.Contains(data);
        }

        public virtual void Remove(TData key)
        {
            base.Remove(key);
        }
    }
}