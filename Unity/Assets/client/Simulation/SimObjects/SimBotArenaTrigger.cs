
using JunkyardDogs.Simulation;
using JunkyardDogs.Simulation.Simulation;
using UnityEngine;

public class SimBotArenaTrigger : SimPhysicsObject
{
    public bool isActive { private set; get; }
    private SimulatedCircleCollider collider;
    public SimBotArenaTrigger(SimulatedEngagement engagement) : base(engagement)
    {
        collider = new SimulatedCircleCollider(body);
        collider.radius = 0.25f;
        colliders.Add(collider);
        body.isTrigger = true;
        collider.gizmosColor = Color.grey;
    }

    public override void OnCollision(SimPhysicsObject other)
    {
        base.OnCollision(other);

        if (other is SimArena)
        {
            isActive = true;
            collider.gizmosColor = Color.green;
        }
    }
    
    public override void OnCollisionExit(SimPhysicsObject other)
    {
        base.OnCollision(other);

        if (other is SimArena)
        {
            isActive = false;
            collider.gizmosColor = Color.grey;
        }
    }
}
