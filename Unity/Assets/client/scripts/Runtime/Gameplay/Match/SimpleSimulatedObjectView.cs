using JunkyardDogs.Simulation;

public abstract class SimpleSimulatedObjectView
{
    protected SimObject simObject;
    protected SimpleSimulatedEngagement viewContainer;
    
    public SimpleSimulatedObjectView(SimpleSimulatedEngagement viewContainer, SimObject simObject)
    {
        this.simObject = simObject;
        this.viewContainer = viewContainer;
    }
    
    public virtual void Update()
    {
        
    }

    public virtual void Destroy()
    {
        
    }
}
