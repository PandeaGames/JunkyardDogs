using JunkyardDogs.Simulation;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Gameplay.Match
{
    public class SimplePulseObjectView : SimpleSimulatedPhysicsObjectView
    {
        private SimPulseAttack _simObject;
        public SimplePulseObjectView(SimpleSimulatedEngagement viewContainer, SimPulseAttack simObject) : base(viewContainer, simObject)
        {
            _simObject = simObject;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(viewContainer.botRenderConfiguration.PulsePrefab);
            return botObject;
        }
            
        public override void Update()
        {
            SimulatedCircleCollider circleCollider = _simObject.collider as SimulatedCircleCollider;
            float localScale = circleCollider.radius * 2;
            scale = new Vector3(localScale, localScale, localScale);
            base.Update();
        }
    }
}