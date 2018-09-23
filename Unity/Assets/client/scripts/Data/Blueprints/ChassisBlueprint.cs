using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChassisBlueprint : PhysicalComponentBlueprint<ChassisBlueprintData>
{
    [Header("Plates")]
    
    [SerializeField]
    private List<PlateBlueprint> _frontPlates;

    [SerializeField]
    private List<PlateBlueprint> _leftPlates;

    [SerializeField]
    private List<PlateBlueprint> _rightPlates;

    [SerializeField]
    private List<PlateBlueprint> _backPlates;

    [SerializeField]
    private List<PlateBlueprint> _topPlates;

    [SerializeField]
    private List<PlateBlueprint> _bottomPlates;

    [Header("Weapons")]
    
    [SerializeField]
    private WeaponProcessorBlueprint _topArmament;

    [SerializeField]
    private WeaponProcessorBlueprint _frontArmament;

    [SerializeField]
    private WeaponProcessorBlueprint _leftArmament;

    [SerializeField]
    private WeaponProcessorBlueprint _rightArmament;
}