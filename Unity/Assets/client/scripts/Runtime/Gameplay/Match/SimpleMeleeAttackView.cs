using JunkyardDogs.Simulation;
using JunkyardDogs.Simulation.Simulation;
using PandeaGames;
using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Gameplay.Match
{
    public class SimpleMeleeAttackView : SimpleSimulatedPhysicsObjectView
    {
        private SimMeleeAttack _simObject;
        public SimpleMeleeAttackView(SimpleSimulatedEngagement viewContainer, SimMeleeAttack simObject) : base(viewContainer, simObject)
        {
            _simObject = simObject;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(SynchronousStaticDataProvider.Instance.GetMeleeImpactArtConfigData(_simObject.Melee).Prefab);
            SyncronizeWithBody(botObject.transform);
            GameObject mainView = new GameObject();
            return mainView;
        }
    }
}