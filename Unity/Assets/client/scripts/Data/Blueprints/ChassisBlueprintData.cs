using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/ChassisBlueprint")]
public class ChassisBlueprintData : PhysicalComponentBlueprintData<Chassis>
{
    [Header("Plates")]
    
    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _frontPlates;

    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _leftPlates;

    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _rightPlates;

    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _backPlates;

    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _topPlates;

    [SerializeField, PlateBlueprintStaticDataReference]
    private List<PlateBlueprintStaticDataReference> _bottomPlates;

    [Header("Weapons")]
    
    [SerializeField, WeaponProcessorBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _topArmament;

    [SerializeField, WeaponBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _frontArmament;

    [SerializeField, WeaponBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _leftArmament;

    [SerializeField, WeaponBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _rightArmament;

    public override Chassis DoGenerate(int seed)
    {
        Chassis chassis = base.DoGenerate(seed);

        FillPlates(_frontPlates, chassis.FrontPlates, seed);
        FillPlates(_leftPlates, chassis.LeftPlates, seed);
        FillPlates(_rightPlates, chassis.RightPlates, seed);
        FillPlates(_backPlates, chassis.BackPlates, seed);
        FillPlates(_topPlates, chassis.TopPlates, seed);
        FillPlates(_bottomPlates, chassis.BottomPlates, seed);

        if (_topArmament.Data != null)
        {
            chassis.TopArmament = _topArmament.Data.DoGenerate(seed);
        }
        
        if (_frontArmament.Data != null)
        {
            chassis.FrontArmament = _frontArmament.Data.DoGenerate(seed);
        }
        
        if (_leftArmament.Data != null)
        {
            chassis.LeftArmament = _leftArmament.Data.DoGenerate(seed);
        }
        
        if (_rightArmament.Data != null)
        {
            chassis.RightArmament = _rightArmament.Data.DoGenerate(seed);
        }

        return chassis;
    }

    private void FillPlates(List<PlateBlueprintStaticDataReference> dataReferences, List<Plate> plates, int seed)
    {
        while (plates.Count > dataReferences.Count)
        {
            plates.RemoveAt(0);
        }
        
        while (plates.Count < dataReferences.Count)
        {
            plates.Add(null);
        }
        
        for (int i = 0; i < dataReferences.Count; i++)
        {
            PlateBlueprintStaticDataReference reference = dataReferences[i];

            if (reference != null)
            {
                plates[i] = reference.Data.DoGenerate(seed);
            }
        }
    }
}