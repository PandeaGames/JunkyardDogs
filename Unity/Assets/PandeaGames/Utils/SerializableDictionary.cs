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

        public KeyValuePair()
        {
            
        }
    }

    public interface ISerializableDictionary<TKey, TValue>
    {
        TValue this[TKey key] { get; set; }
        void Add(TKey key, TValue value);
        void Clear();
        bool Contains(TKey key);
        void Remove(TKey key);
    }
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue, TPair> where TPair:KeyValuePair<TKey, TValue>, new()
    {
        [SerializeField, ReorderableList]
        private List<TPair> _keyValuePairs;

        public List<TPair> KeyValuePairs
        {
            get { return _keyValuePairs; }
            set { _keyValuePairs = value; }
        }
        
        public SerializableDictionary()
        {
            _keyValuePairs = new List<TPair>();
        }
        
        public virtual void Add(TKey key, TValue value)
        {
            if (Contains(key))
            {
                throw new ArgumentException("An element with the same key already exists in the IDictionary object.");
            }
            
            _keyValuePairs.Add(new TPair { Value = value, Key = key });
        }

        public virtual void Clear()
        {
            _keyValuePairs.Clear();
        }

        public virtual bool Contains(TKey key)
        {
            return Contains(key as object);
        }

        protected bool Contains(object obj)
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
            Remove(key as object);
        }
        
        protected virtual void Remove(object key)
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
        
        public virtual TValue this[TKey key]
        {
            get
            {
                return this[key as object];
            }
            set
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
                
                Add(key, value);
            }
        }

        protected virtual TValue this[object obj]
        {
            get
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
            set
            {
                int hash = obj.GetHashCode();
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
}