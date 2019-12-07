using UnityEngine.Scripting;

namespace JunkyardDogs.Simulation
{
    [Preserve]
    public class DecisionMoveLeft : DecisionStrafe
    {
        public DecisionMoveLeft() : base(StrafeDirection.Left)
        {
            
        }
    }
}