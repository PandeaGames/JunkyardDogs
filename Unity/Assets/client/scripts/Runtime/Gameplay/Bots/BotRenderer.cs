using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using Chassis = JunkyardDogs.Components.Chassis;

public class BotRenderer : MonoBehaviour
{
    public void Render(Bot bot, PrefabFactory componentFactory, ScriptableObjectFactory avatarFactory, MaterialFactory materials, UnityEngine.Material missingComponentMaterial)
    {
        BotAvatar botAvatar = avatarFactory.GetAsset(bot.Chassis.Specification) as BotAvatar;
        Chassis chassis = bot.Chassis;

        RenderPlates(Chassis.PlateLocation.Bottom, chassis, botAvatar, materials, missingComponentMaterial);
        RenderPlates(Chassis.PlateLocation.Front, chassis, botAvatar, materials, missingComponentMaterial);
        RenderPlates(Chassis.PlateLocation.Left, chassis, botAvatar, materials, missingComponentMaterial);
        RenderPlates(Chassis.PlateLocation.Right, chassis, botAvatar, materials, missingComponentMaterial);
        RenderPlates(Chassis.PlateLocation.Top, chassis, botAvatar, materials, missingComponentMaterial);

        RenderArmament(bot, Chassis.ArmamentLocation.Front, componentFactory, chassis, botAvatar, materials, missingComponentMaterial);
        RenderArmament(bot, Chassis.ArmamentLocation.Left, componentFactory, chassis, botAvatar, materials, missingComponentMaterial);
        RenderArmament(bot, Chassis.ArmamentLocation.Right, componentFactory, chassis, botAvatar, materials, missingComponentMaterial);
        RenderArmament(bot, Chassis.ArmamentLocation.Top, componentFactory, chassis, botAvatar, materials, missingComponentMaterial);

        Renderer renderer = transform.GetChild(botAvatar.Frame.transform.GetSiblingIndex()).GetComponent<Renderer>();
        renderer.material = materials.GetAsset(bot.Chassis.Material.Asset);
    }

    private void RenderArmament(Bot bot, Chassis.ArmamentLocation location, PrefabFactory componentFactory, Chassis chasiss, BotAvatar botAvatar, MaterialFactory materials, UnityEngine.Material missingComponentMaterial)
    {
        GameObject armamentContainer = botAvatar.GetArmamentContainer(location);

        if(armamentContainer != null)
        {
            foreach (Transform child in armamentContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Renderer renderer = armamentContainer.GetComponent<Renderer>();
            renderer.enabled = false;

            WeaponProcessor processor = bot.Chassis.GetWeaponProcessor(location);

            if(processor != null)
            {
                var weapon = processor.Weapon;
                GameObject obj = null;

                if(weapon == null)
                {
                    obj = componentFactory.InstantiateAsset(processor.Specification);
                }
                else
                {
                    obj = componentFactory.InstantiateAsset(weapon.Specification);
                }

                obj.transform.SetParent(transform.GetChild(armamentContainer.transform.GetSiblingIndex()), false);
            }
        }
    }

    private void RenderPlates(Chassis.PlateLocation plateLocation, Chassis chassis, BotAvatar botAvatar, MaterialFactory materials, UnityEngine.Material missingComponentMaterial)
    {
        List<JunkyardDogs.Components.Plate> plates = chassis.GetPlateList(plateLocation);
        List<GameObject> avatarPlates = botAvatar.GetPlateList(plateLocation);

        for(int i = 0; i<avatarPlates.Count;i ++)
        {
            GameObject avatarPlate = avatarPlates[i];
            int index = avatarPlate.transform.GetSiblingIndex();
            bool hasPlate = plates.Count > i && plates[i] != null;
            Transform plate = transform.GetChild(index);
            Renderer plateRenderer = plate.GetComponent<Renderer>();

            if(hasPlate)
            {
                JunkyardDogs.Components.Plate plateComponent = plates[i];
                plateRenderer.material = materials.GetAsset(plateComponent.Material.Asset);
            }
            else
            {
                plateRenderer.material = missingComponentMaterial;
            }
        }
    }
}
