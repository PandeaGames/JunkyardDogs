using System;
using UnityEngine;

namespace JunkyardDogs.Components.Gameplay
{
    [Serializable]
    public class ExpLevel : Exp
    {
        [SerializeField]
        private uint _level;
        public uint Level
        {
            get => _level;
        }

        public ExpLevel() : this(0)
        {
            
        }
        
        public ExpLevel(int exp) : this(0, exp)
        {
            
        }
        
        public ExpLevel(uint level) : this(0, 0)
        {
        }
        
        public ExpLevel(uint level, int exp) : base(exp)
        {
            _level = level;
        }

        public uint Ascend()
        {
            _value = 0;
            return ++_level;
        }
    }
}