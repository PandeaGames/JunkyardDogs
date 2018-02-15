using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation.Behavior
{
    public enum WeaponSlot
    {
        RIGHT,
        LEFT,
        TOP,
        FRONT
    }

    [CreateAssetMenu(fileName = "Attack", menuName = "Simulation/Behavior/Attack", order = 2)]
    public class Attack : Action
    {
        [SerializeField]
        private WeaponSlot _weaponSlot;

        public WeaponSlot WeaponSlot { get { return _weaponSlot; } }

        public override ActionResult GetResult()
        {
            ActionResult result = default(ActionResult);
            result.attack = true;
            result.weaponSlot = _weaponSlot;

            return result;
        }
    }
}