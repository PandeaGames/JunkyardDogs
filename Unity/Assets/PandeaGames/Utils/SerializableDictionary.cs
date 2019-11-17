using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace PandeaGames.Utils
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        [SerializeField]
        private TKey _key;
            
        [SerializeField]
        private TValue _value;
            
        public TKey Key
        {
            get { return _key; }
            set { _key = value; }
        }
            
        public TValue Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public interface ISerializableDictionary<TKey, TValue, TPair> where TPair:KeyValuePair<TKey, TValue>
    {
        TValue GetValue(TKey key);
        void SetValue(TKey key, TValue value);
        TPair AddValue(TKey key, TValue value);
        void Clear();
        bool Contains(TKey key);
        void Remove(TKey key);
    }
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue, TPair> 
        : ISerializableDictionary<TKey, TValue, TPair> where TPair:KeyValuePair<TKey, TValue>,
        new()
    {
        [SerializeField] 
        protected List<TPair> _keyValuePairs = new List<TPair>();

        public virtual int Count
        {
            get
            {
                return _keyValuePairs.Count;
            }
        }

        public List<TPair> KeyValuePairs
        {
            get { return _keyValuePairs; }
            set { _keyValuePairs = value; }
        }

        public virtual TPair AddValue(TKey key, TValue value)
        {
            if (Contains(key))
            {
                throw new ArgumentException("An element with the same key already exists in the IDictionary object.");
            }

            TPair newPair = new TPair {Value = value, Key = key};
            _keyValuePairs.Add(newPair);
            return newPair;
        }

        public virtual void Clear()
        {
            _keyValuePairs.Clear();
        }

        public virtual bool Contains(TKey key)
        {
            return ContainsObj(key);
        }

        protected bool ContainsObj(object obj)
        {
            int hashCode = obj.GetHashCode();
            foreach (KeyValuePair<TKey, TValue> kvp in _keyValuePairs)
            {
                int searchingHashCode = kvp.Key.GetHashCode();
                if (searchingHashCode == hashCode)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void Remove(TKey key)
        {
            RemoveObj(key as object);
        }
        
        protected virtual void RemoveObj(object key)
        {
            int hash = key.GetHashCode();
            
            int foundIndex = -1;
            
            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                TPair kvp = _keyValuePairs[i];
                if (kvp.Key.GetHashCode() == hash)
                {
                    foundIndex = i;
                    break;
                }
            }

            if(foundIndex >=0)
            {
                _keyValuePairs.RemoveAt(foundIndex);
            }
        }

        public virtual TPair GetPair(int index)
        {
            return _keyValuePairs[index];
        }
        
        public virtual TPair GetPair(TKey key)
        {
            foreach (TPair pair in _keyValuePairs)
            {
                if (pair.Key.Equals(key))
                {
                    return pair;
                }
            }
            
            return default(TPair);
        }

        public virtual TValue GetValue(TKey key)
        {
            return GetValueByObj(key as object);
        }
        public virtual void SetValue(TKey key, TValue value)
        {
            int hash = key.GetHashCode();
            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                TPair kvp = _keyValuePairs[i];
                if (kvp.Key.GetHashCode() == hash)
                {
                    kvp.Value = value;
                    return;
                }
            }
            
            AddValue(key, value);
        }

        protected virtual TValue GetValueByObj(object obj)
        {
            int hash = obj.GetHashCode();
            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                TPair kvp = _keyValuePairs[i];
                if (kvp.Key.GetHashCode() == hash)
                {
                    return kvp.Value;
                }
            }

            return default(TValue);
        }

        protected virtual void SetValueByObj(object obj, TValue value)
        {
            int hash = obj.GetHashCode();
            SetValueByHash(hash, value);
            _keyValuePairs.Add(new TPair { Value = value, Key = (TKey)obj });
        }
        
        protected virtual void SetValueByHash(int hash, TValue value)
        {
            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                TPair kvp = _keyValuePairs[i];
                if (kvp.Key.GetHashCode() == hash)
                {
                    kvp.Value = value;
                    return;
                }
            }
        }
    }
}