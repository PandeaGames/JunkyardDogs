using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public class DecisionMoveRight : DecisionStrafe
    {
        public DecisionMoveRight() : base(StrafeDirection.Right)
        {

        }
    }
}