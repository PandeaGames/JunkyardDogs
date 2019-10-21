using JunkyardDogs.Simulation;
using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Gameplay.Match
{

    public class SimpleLaser : SimpleSimulatedPhysicsObjectView
    {
        private SimHitscanShot _simObject;
        public SimpleLaser(SimpleSimulatedEngagement viewContainer, SimHitscanShot simObject) : base(viewContainer, simObject)
        {
            _simObject = simObject;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(viewContainer.botRenderConfiguration.LaserPrefab);
            
            return botObject;
        }
    }
}