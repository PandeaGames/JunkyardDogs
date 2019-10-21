using JunkyardDogs.Simulation;
using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Gameplay.Match
{

    public class SimpleMachineGun : SimpleSimulatedPhysicsObjectView
    {
        private SimHitscanShot _simObject;
        public SimpleMachineGun(SimpleSimulatedEngagement viewContainer, SimHitscanShot simObject) : base(viewContainer, simObject)
        {
            _simObject = simObject;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(viewContainer.botRenderConfiguration.MachineGunPrefab);
            
            return botObject;
        }
    }
}