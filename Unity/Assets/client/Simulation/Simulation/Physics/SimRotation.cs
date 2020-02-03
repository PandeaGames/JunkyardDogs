using System;
using UnityEngine;

namespace JunkyardDogs.Simulation.Simulation
{
    public struct SimRotation
    {
        // Overloading of Binary "+" operator 
        public static SimRotation operator + (SimRotation r1,  
            SimRotation r2) 
        { 
            SimRotation r = new SimRotation();
            r.deg360 = r1.deg360 + r2.deg360;
            return r; 
        } 
        
        private float _rad;
        private float _radFull;
        private float _deg;
        private float _deg360;

        public float rad
        {
            get { return _rad; }
            set
            {
                _rad = value;
                _radFull = _rad;
                
                if (_radFull < 0)
                {
                    _radFull += Mathf.PI * 2;
                }
                
                _deg = _rad * Mathf.Rad2Deg;
                _deg360 = _radFull * Mathf.Rad2Deg;
            }
        }
        
        public float deg
        {
            get { return _deg; }
        }

        public float deg360
        {
            get { return _deg360; }
            set
            {
                if (value < 0)
                {
                    value %= 360;
                    value += 360;
                }
                
                if (value > 360)
                {
                    value %= 360;
                }

                if (value > 180)
                {
                    value = (180 - (value - 180)) * -1;
                }

                value *= Mathf.Deg2Rad;
                rad = value;
            }
        }

        public float radFull
        {
            get { return _radFull; }
        }
        
        public void Set(Vector2 vector)
        {
            Set(vector.x, vector.y);
        }

        public void Set(float x, float y)
        {
            rad = Mathf.Atan2(y, x);
        }

        public void SetFromToRotation(Vector2 from, Vector2 to)
        {
            to += AdjustToPositiveSector(ref from);
            from += AdjustToPositiveSector(ref to);
            Set(to - from);
        }

        private Vector2 AdjustToPositiveSector(ref Vector2 input)
        {
            Vector2 adjustment = Vector2.zero;

            if (input.x < 0)
            {
                adjustment.x = input.x * -1;
                input.x = 0;
            }
            
            if (input.y < 0)
            {
                adjustment.y = input.y * -1;
                input.y = 0;
            }

            return adjustment;
        }

        public static implicit operator Quaternion(SimRotation r)
        {
            return Quaternion.Euler(new Vector3(0, r.deg, 0));
        }
    }
}