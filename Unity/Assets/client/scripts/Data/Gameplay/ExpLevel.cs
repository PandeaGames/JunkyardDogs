using System;
using UnityEngine;

namespace JunkyardDogs.Components.Gameplay
{
    [Serializable]
    public class ExpLevel : Exp
    {
        [SerializeField]
        private uint _level = 1;
        public uint Level
        {
            get { 
                if (_level <= 0)
                {
                    _level = 1;
                }
                
                return _level; 
            }
            set => _level = value;
        }

        public ExpLevel() : this(1)
        {
            
        }
        
        public ExpLevel(int exp) : this(1, exp)
        {
            
        }
        
        public ExpLevel(uint level) : this(1, 0)
        {
        }
        
        public ExpLevel(uint level, int exp) : base(exp)
        {
            _level = level;
        }

        public uint Ascend()
        {
            _value = 0;
            _level += 1;
            return _level;
        }
    }
}