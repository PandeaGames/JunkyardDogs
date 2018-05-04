using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;

public class ChassisRenderer : ComponentRenderer
{
    public override void RenderComponent(Component component)
    {
        Chassis chassis = component as Chassis;

        if (chassis != null)
        {
            RenderChassis(chassis);
        }
    }

    private void RenderChassis(Chassis chassis)
    {

    }
}
