using System;
using System.Collections.Generic;
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
    
    [Serializable]
    public class SerializableDictionary<TKey, TValue, TPair> where TPair:KeyValuePair<TKey, TValue>
    {
        [SerializeField]
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
    }
}