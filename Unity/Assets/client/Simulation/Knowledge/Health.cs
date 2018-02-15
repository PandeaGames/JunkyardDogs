using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge
{
    [CreateAssetMenu(fileName = "Health", menuName = "Simulation/Knowledge/Health", order = 2)]
    public class Health : Knowledge
    {
        [SerializeField, Range(0.0f, 1.0f)]
        private float _healthBoundry;

        public float HealthBoundry { get { return _healthBoundry; } }

        public override bool IsTrue(Information.Information information)
        {
            return information.Self.Health / information.Self.TotalHealth < _healthBoundry;
        }
    }
}