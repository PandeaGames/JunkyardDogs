using JunkyardDogs.Components;
using UnityEngine;

public static class LootUtilities
{
    public static IComponent TryCreateComponentFromLoot(ILoot loot, int seed)
    {
         if(loot is WeaponBlueprintData)
        {
            return (loot as WeaponBlueprintData).DoGenerate(seed);
        }
        else if(loot is WeaponProcessorBlueprintData)
        {
            return (loot as WeaponProcessorBlueprintData).DoGenerate(seed);
        }
        else if(loot is ChassisBlueprintData)
        {
            return (loot as ChassisBlueprintData).DoGenerate(seed);
        }
        else if(loot is PlateBlueprintData)
        {
            return (loot as PlateBlueprintData).DoGenerate(seed);
        }
        else if(loot is MotherboardBlueprintData)
        {
            return (loot as MotherboardBlueprintData).DoGenerate(seed);
        }
        else if(loot is CPUBlueprintData)
         {
             return (loot as CPUBlueprintData).DoGenerate(seed);
         }
         else if(loot is DirectiveBlueprintData)
         {
             return (loot as DirectiveBlueprintData).DoGenerate(seed);
         }
        else
        {
            Debug.LogWarning("LootUtilities failed to create component for loot "+loot);
            return null;
        }
    }
}
