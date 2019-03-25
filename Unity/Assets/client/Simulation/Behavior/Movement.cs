using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Behavior
{
    public enum MovementDirection
    {
        RIGHT,
        LEFT, 
        TOWARDS, 
        AWAY
    }

    [CreateAssetMenu(fileName = "Movement", menuName = "Simulation/Behavior/Movement", order = 2)]
    public class Movement : BehaviorAction
    {
        [SerializeField]
        private MovementDirection _movementDirection;

        public MovementDirection MovementDirection { get { return _movementDirection; } }

        public override ActionResult GetResult()
        {
            ActionResult result = default(ActionResult);
            result.movement = Vector2.zero;

            switch (_movementDirection)
            {
                case MovementDirection.RIGHT:
                    result.movement.Set(1, 0);
                    break;
                case MovementDirection.LEFT:
                    result.movement.Set(-1, 0);
                    break;
                case MovementDirection.TOWARDS:
                    result.movement.Set(0, 1);
                    break;
                case MovementDirection.AWAY:
                    result.movement.Set(0, -1);
                    break;
            }

            return result;
        }
    }
}