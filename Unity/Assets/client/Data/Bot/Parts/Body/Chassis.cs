using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JunkyardDogs.Bot
{
    public class Chassis : BodyComponent
    {
        [SerializeField]
        private Plate[] _frontPlates;

        [SerializeField]
        private Plate[] _leftPlates;

        [SerializeField]
        private Plate[] _rightPlates;

        [SerializeField]
        private Plate[] _backPlates;

        [SerializeField]
        private Plate[] _topPlates;


    }
}