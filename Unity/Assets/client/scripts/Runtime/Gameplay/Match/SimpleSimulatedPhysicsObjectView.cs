using JunkyardDogs.Simulation;
using UnityEngine;

public abstract class SimpleSimulatedPhysicsObjectView : SimpleSimulatedObjectView
{
    protected SimPhysicsObject simPhysicsObject;
    public GameObject view;
    protected Vector3 scale = Vector3.one;
    
    public SimpleSimulatedPhysicsObjectView(SimpleSimulatedEngagement viewContainer, SimObject simObject) : base(viewContainer, simObject)
    {
        simPhysicsObject = simObject as SimPhysicsObject;
        
    }

    public void Make()
    {
        this.view = GenerateView();
    }

    protected abstract GameObject GenerateView();

    public override void Update()
    {
        SyncronizeWithBody(view.transform);
    }

    protected void SyncronizeWithBody(Transform transform)
    {
        transform.position = new Vector3(simPhysicsObject.body.position.x, 0, simPhysicsObject.body.position.y);
        transform.rotation = Quaternion.Euler(0, -90 - (simPhysicsObject.body.rotation.deg360 +90), 0);
        transform.localScale = scale;
    }
    
    
    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(view);
    }
}
