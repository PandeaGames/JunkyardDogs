using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Specifications
{
    [CreateAssetMenu(fileName = "Chassis", menuName = "Specifications/Chassis", order = 5)]
    public class Chassis : PhysicalSpecification
    {
        [SerializeField]
        private float _maxSpeedPerSecond;

        [SerializeField]
        private int _frontPlates;

        [SerializeField]
        private int _leftPlates;

        [SerializeField]
        private int _rightPlates;

        [SerializeField]
        private int _backPlates;

        [SerializeField]
        private int _topPlates;

        [SerializeField]
        private int _bottomPlates;

        [SerializeField]
        private bool _topArmament;

        [SerializeField]
        private bool _frontArmament;

        [SerializeField]
        private bool _leftArmament;

        [SerializeField]
        private bool _rightArmament;

        public float MaxSpeedPerSecond { get { return _maxSpeedPerSecond; } }

        public int FrontPlates { get { return _frontPlates; } }
        public int LeftPLates { get { return _leftPlates; } }
        public int RightPlates { get { return _rightPlates; } }
        public int BackPlates { get { return _backPlates; } }
        public int BottomPlates { get { return _bottomPlates; } }

        public bool TopArmament { get { return _topArmament; } }
        public bool FrontArmament { get { return _frontArmament; } }
        public bool LeftArmament { get { return _leftArmament; } }
        public bool RightArmament { get { return _rightArmament; } }
    }
}