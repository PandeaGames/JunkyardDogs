using System.Collections.Generic;
using System.Linq;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/ChassisBlueprint")]
public class ChassisBlueprintData : PhysicalComponentBlueprintData<Chassis>, IStaticDataBalance<ChassisBlueprintBalanceObject>
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

    [SerializeField, WeaponProcessorBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _frontArmament;

    [SerializeField, WeaponProcessorBlueprintStaticDataReference]
    private WeaponProcessorBlueprintStaticDataReference _leftArmament;

    [SerializeField, WeaponProcessorBlueprintStaticDataReference]
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

    public void ApplyBalance(ChassisBlueprintBalanceObject balance)
    {
        name = balance.name;
        
        _specification = new SpecificationStaticDataReference();
        _specification.ID = balance.specification;
        _manufacturer = new ManufacturerStaticDataReference();
        _manufacturer.ID = balance.manufacturer;
        Material = new MaterialStaticDataReference();
        Material.ID = balance.material;
        
        _topArmament = new WeaponProcessorBlueprintStaticDataReference();
        _topArmament.ID = balance.topArmament;
        _leftArmament = new WeaponProcessorBlueprintStaticDataReference();
        _leftArmament.ID = balance.leftArmament;
        _rightArmament = new WeaponProcessorBlueprintStaticDataReference();
        _rightArmament.ID = balance.rightArmament;
        _frontArmament = new WeaponProcessorBlueprintStaticDataReference();
        _frontArmament.ID = balance.frontArmament;

        ApplyPlateBalance(balance.frontPlates, out _frontPlates);
        ApplyPlateBalance(balance.leftPlates, out _leftPlates);
        ApplyPlateBalance(balance.rightPlates, out _rightPlates);
        ApplyPlateBalance(balance.backPlates, out _backPlates);
        ApplyPlateBalance(balance.topPlates, out _topPlates);
        ApplyPlateBalance(balance.bottomPlates, out _bottomPlates);
    }

    public ChassisBlueprintBalanceObject GetBalance()
    {
        ChassisBlueprintBalanceObject balance = new ChassisBlueprintBalanceObject();
        balance.name = name;
        balance.specification = _specification == null ? string.Empty : _specification.ID;
        balance.manufacturer = _manufacturer == null ? string.Empty : _manufacturer.ID;
        

        balance.topArmament = _topArmament == null ? string.Empty : _topArmament.ID;
        balance.leftArmament = _leftArmament == null ? string.Empty : _leftArmament.ID;
        balance.rightArmament = _rightArmament == null ? string.Empty : _rightArmament.ID;
        balance.frontArmament = _frontArmament == null ? string.Empty : _frontArmament.ID;

        balance.frontPlates = string.Join(BalanceData.ListDelimiter, _frontPlates);
        balance.leftPlates = string.Join(BalanceData.ListDelimiter, _leftPlates);
        balance.rightPlates = string.Join(BalanceData.ListDelimiter, _rightPlates);
        balance.backPlates = string.Join(BalanceData.ListDelimiter, _backPlates);
        balance.topPlates = string.Join(BalanceData.ListDelimiter, _topPlates);
        balance.bottomPlates = string.Join(BalanceData.ListDelimiter, _bottomPlates);
        
        return balance;
    }

    private void ApplyPlateBalance(string balanceInput, out List<PlateBlueprintStaticDataReference> plates)
    {
        plates = new List<PlateBlueprintStaticDataReference>();
        string[] balanceInputList = balanceInput.Split(BalanceData.ListDelimiterChar);

        foreach (string plateId in balanceInputList)
        {
            PlateBlueprintStaticDataReference reference = new PlateBlueprintStaticDataReference();
            reference.ID = plateId;
            plates.Add(reference);
        }
    }
}