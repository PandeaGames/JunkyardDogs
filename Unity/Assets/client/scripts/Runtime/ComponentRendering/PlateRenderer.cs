using JunkyardDogs.Components;

public class PlateRenderer : ComponentRenderer
{
    public override void RenderComponent(IComponent component)
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
