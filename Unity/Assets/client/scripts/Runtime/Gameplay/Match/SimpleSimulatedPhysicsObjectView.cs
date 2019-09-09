using JunkyardDogs.Simulation;
using UnityEngine;

public abstract class SimpleSimulatedPhysicsObjectView : SimpleSimulatedObjectView
{
    protected SimPhysicsObject simPhysicsObject;
    protected GameObject view;
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
        view.transform.position = new Vector3(simPhysicsObject.body.position.x, 0, simPhysicsObject.body.position.y);
        view.transform.rotation = Quaternion.Euler(0, -90 - (simPhysicsObject.body.rotation.deg360 +90), 0);
        view.transform.localScale = scale;
    }
    
    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(view);
    }
}
