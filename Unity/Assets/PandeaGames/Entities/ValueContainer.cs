using System;
using UnityEngine;

namespace PandeaGames.Entities
{
    [Serializable]
    public abstract class ValueContainer<TValueContainer, TValue> where TValueContainer:ValueContainer<TValueContainer, TValue>
    {
        [SerializeField]
        protected TValue _value;
        public TValue Value
        {
            get { return _value; }
            set { _value = value; }
        }
        
        public ValueContainer(TValue value)
        {
            _value = value;
        }

        public ValueContainer()
        {
            
        }

        public abstract TValueContainer Add(TValueContainer other);

        public static TValueContainer operator + (ValueContainer<TValueContainer, TValue> container1,  
            TValueContainer container2)
        {
            return container1.Add(container2);
        }
        
        public static bool operator != (ValueContainer<TValueContainer, TValue> container1,  
            ValueContainer<TValueContainer, TValue> container2) 
        {
            return !container1._value.Equals(container2._value); 
        }
        
        public static bool operator == (ValueContainer<TValueContainer, TValue> container1,  
            ValueContainer<TValueContainer, TValue> container2) 
        {
            return container1._value.Equals(container2._value); 
        } 
    }
}