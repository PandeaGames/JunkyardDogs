using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge
{
    [CreateAssetMenu(fileName = "Range", menuName = "Simulation/Knowledge/Range", order = 2)]
    public class Range : Knowledge
    {
        [SerializeField]
        private float _radius;

        public float Radius { get { return _radius; } }

        public override bool IsTrue(Information.Information information)
        {
            float dist = Vector2.Distance(information.Self.Position, information.Opponent.Position);

            return dist < _radius;
        }
    }
}