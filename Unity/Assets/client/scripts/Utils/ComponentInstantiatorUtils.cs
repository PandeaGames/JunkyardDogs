using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using System;

public class ComponentInstantiatorUtils
{
    public static JunkyardDogs.Components.GenericComponent GenerateComponent(Specification spec)
    {
        JunkyardDogs.Components.GenericComponent component = null;

        if(spec is JunkyardDogs.Specifications.Weapon)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.Weapon>();

            JunkyardDogs.Specifications.Weapon weaponSpec = spec as JunkyardDogs.Specifications.Weapon;
            JunkyardDogs.Components.Weapon weaponComp = component as JunkyardDogs.Components.Weapon;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.Chassis)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.Chassis>();

            JunkyardDogs.Specifications.Chassis weaponSpec = spec as JunkyardDogs.Specifications.Chassis;
            JunkyardDogs.Components.Chassis weaponComp = component as JunkyardDogs.Components.Chassis;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.WeaponChip)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.WeaponProcessor>();

            JunkyardDogs.Specifications.WeaponChip weaponSpec = spec as JunkyardDogs.Specifications.WeaponChip;
            JunkyardDogs.Components.WeaponProcessor weaponComp = component as JunkyardDogs.Components.WeaponProcessor;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.SubProcessor)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.SubProcessor>();

            JunkyardDogs.Specifications.SubProcessor weaponSpec = spec as JunkyardDogs.Specifications.SubProcessor;
            JunkyardDogs.Components.SubProcessor weaponComp = component as JunkyardDogs.Components.SubProcessor;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.Plate)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.Plate>();

            JunkyardDogs.Specifications.Plate weaponSpec = spec as JunkyardDogs.Specifications.Plate;
            JunkyardDogs.Components.Plate weaponComp = component as JunkyardDogs.Components.Plate;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.CPU)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.CPU>();

            JunkyardDogs.Specifications.CPU weaponSpec = spec as JunkyardDogs.Specifications.CPU;
            JunkyardDogs.Components.CPU weaponComp = component as JunkyardDogs.Components.CPU;

            weaponComp.Specification = weaponSpec;
        }
        else if (spec is JunkyardDogs.Specifications.CircuitBoard)
        {
            component = ScriptableObject.CreateInstance<JunkyardDogs.Components.CircuitBoard>();

            JunkyardDogs.Specifications.CircuitBoard weaponSpec = spec as JunkyardDogs.Specifications.CircuitBoard;
            JunkyardDogs.Components.CircuitBoard weaponComp = component as JunkyardDogs.Components.CircuitBoard;

            weaponComp.Specification = weaponSpec;
        }

        return component;
    }
}