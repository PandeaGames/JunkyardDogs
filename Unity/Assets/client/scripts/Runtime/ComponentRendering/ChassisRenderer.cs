using UnityEngine;
using JunkyardDogs.Components;
using System.Collections.Generic;

public class ChassisRenderer : ComponentRenderer
{
    [SerializeField]
    public Transform Chassis;

    [SerializeField]
    public List<PlateRenderer> FrontPlates;

    [SerializeField]
    public List<PlateRenderer> LeftPlates;

    [SerializeField]
    public List<PlateRenderer> RightPlates;

    [SerializeField]
    public List<PlateRenderer> BackPlates;

    [SerializeField]
    public List<PlateRenderer> TopPlates;

    [SerializeField]
    public List<PlateRenderer> BottomPlates;

    [SerializeField]
    public Transform TopArmament;

    [SerializeField]
    public Transform FrontArmament;

    [SerializeField]
    public Transform LeftArmament;

    [SerializeField]
    public Transform RightArmament;

    public override void RenderComponent(IComponent component)
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
