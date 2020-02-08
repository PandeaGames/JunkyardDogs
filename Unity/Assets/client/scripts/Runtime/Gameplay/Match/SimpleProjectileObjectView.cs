using JunkyardDogs.Simulation;
using JunkyardDogs.Simulation.Simulation;
using PandeaGames;
using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Gameplay.Match
{
    public class SimpleProjectileObjectView : SimpleSimulatedPhysicsObjectView
    {
        private SimProjectileAttackObject _simObject;
        public SimpleProjectileObjectView(SimpleSimulatedEngagement viewContainer, SimProjectileAttackObject simObject) : base(viewContainer, simObject)
        {
            _simObject = simObject;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(
                    SynchronousStaticDataProvider.Instance.GetProjectileArtConfigData(_simObject.projectileWeapon).Prefab);
            return botObject;
        }
    }
}