using UnityEngine;
using Component = JunkyardDogs.Components.Component;

public static class LootUtilities
{
    public static Component TryCreateComponentFromLoot(ILoot loot, int seed)
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
        else
        {
            return null;
        }
    }
}
