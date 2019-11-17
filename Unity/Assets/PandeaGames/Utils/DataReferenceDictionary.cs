using System;
using PandeaGames.Data.Static;

namespace PandeaGames.Utils
{
    [Serializable]
    public class DataReferenceDictionaryKVP<TReference,TValue> : KeyValuePair<TReference, TValue>
    {
        
    }
    
    [Serializable]
    public class DataReferenceDictionary<TReference, TData, TDataBase, TDirectory, TValue, TKvP> : SerializableDictionary<TReference, TValue, TKvP>
        where TDataBase:IStaticData
        where TData:TDataBase
        where TKvP:KeyValuePair<TReference, TValue>, new()
        where TReference:StaticDataReference<TDataBase, TData, TReference, TDirectory>, new()
        where TDirectory:StaticDataReferenceDirectory<TDataBase, TData, TReference, TDirectory>, new()
    {
        public virtual TValue GetValue(TData key)
        {
            if (!Contains(key))
            {
                AddValue(key, default(TValue));
            }
            
            return GetValueByObj(key);
        }

        public void SetValue(TData key, TValue value)
        {
            base.SetValueByHash(key.GetHashCode(), value);
        }

        public virtual TKvP GetPair(TData data)
        {
            return GetPair(data.ID);
        }

        public virtual TKvP GetPair(TReference reference)
        {
            return GetPair(reference.ID);
        }

        public virtual TKvP GetPair(string id)
        {
            foreach (TKvP pair in _keyValuePairs)
            {
                if (pair.Key.ID.Equals(id))
                {
                    return pair;
                }
            }
            
            TReference reference = new TReference();
            reference.ID = id;
            return base.AddValue(reference, default(TValue));
        }

        public virtual TKvP AddValue(TData key, TValue value)
        {
            TReference reference = new TReference();
            reference.ID = key.ID;
            return base.AddValue(reference, value);
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