using UnityEngine;
using System.Collections;
using Component = JunkyardDogs.Components.Component;
using JunkyardDogs.Components;

public class PlateRenderer : ComponentRenderer
{
    public override void RenderComponent(Component component)
    {
        Plate plate = component as Plate;

        if (plate != null)
        {
            RenderPlate(plate);
        }
    }

    private void RenderPlate(Plate plate)
    {

    }
}
