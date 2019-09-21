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
            scale = new Vector3(_simObject.body.scale.x, _simObject.body.scale.x, _simObject.body.scale.y);
            base.Update();
        }
    }
}