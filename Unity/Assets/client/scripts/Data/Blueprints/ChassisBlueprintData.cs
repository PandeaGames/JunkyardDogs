using System.Collections.Generic;
using JunkyardDogs.Components;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/ChassisBlueprint")]
public class ChassisBlueprintData : PhysicalComponentBlueprintData<Chassis>
{
    [Header("Plates")]
    
    [SerializeField]
    private List<PlateBlueprintData> _frontPlates;

    [SerializeField]
    private List<PlateBlueprintData> _leftPlates;

    [SerializeField]
    private List<PlateBlueprintData> _rightPlates;

    [SerializeField]
    private List<PlateBlueprintData> _backPlates;

    [SerializeField]
    private List<PlateBlueprintData> _topPlates;

    [SerializeField]
    private List<PlateBlueprintData> _bottomPlates;

    [Header("Weapons")]
    
    [SerializeField]
    private WeaponProcessorBlueprintData _topArmament;

    [SerializeField]
    private WeaponProcessorBlueprintData _frontArmament;

    [SerializeField]
    private WeaponProcessorBlueprintData _leftArmament;

    [SerializeField]
    private WeaponProcessorBlueprintData _rightArmament;

    public override Chassis DoGenerate(int seed)
    {
        throw new System.NotImplementedException();
    }
}