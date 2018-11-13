using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using Chassis = JunkyardDogs.Components.Chassis;
using Material = UnityEngine.Material;

public class BotRenderer : MonoBehaviour
{
    public void Render(Bot bot, BotRenderConfiguration renderConfiguration)
    {
        BotAvatar botAvatar = renderConfiguration.AvatarFactory.GetAsset(bot.Chassis.Specification) as BotAvatar;
        Chassis chassis = bot.Chassis;

        RenderPlates(Chassis.PlateLocation.Bottom, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Front, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Left, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Right, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Top, chassis, botAvatar, renderConfiguration);

        RenderArmament(bot, Chassis.ArmamentLocation.Front, renderConfiguration.ComponentFactory, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Left, renderConfiguration.ComponentFactory, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Right, renderConfiguration.ComponentFactory, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Top, renderConfiguration.ComponentFactory, chassis, botAvatar, renderConfiguration);

        Renderer renderer = transform.GetChild(botAvatar.Frame.transform.GetSiblingIndex()).GetComponent<Renderer>();

        bot.Chassis.Material.LoadAssetAsync<ScriptableObject>((asset, reference) =>
        {
            renderer.material = renderConfiguration.Materials.GetAsset(asset);
        }, (e) => { });
    }

    private void RenderArmament(Bot bot, Chassis.ArmamentLocation location, PrefabFactory componentFactory, Chassis chasiss, BotAvatar botAvatar, BotRenderConfiguration renderConfiguration)
    {
        GameObject avatarArmamentContainer = botAvatar.GetArmamentContainer(location);
        GameObject armamentContainer = null;

        if(avatarArmamentContainer != null)
        {
            armamentContainer = transform.GetChild(avatarArmamentContainer.transform.GetSiblingIndex()).gameObject;
        }

        if (armamentContainer != null)
        {
            foreach (Transform child in armamentContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Renderer renderer = armamentContainer.GetComponent<Renderer>();
            renderer.material = renderConfiguration.MissingComponentMaterial;

            WeaponProcessor processor = bot.Chassis.GetWeaponProcessor(location);

            if(processor != null)
            {
                var weapon = processor.Weapon;
                GameObject obj = null;

                if(weapon == null)
                {
                    renderer.enabled = renderConfiguration.MissingComponentMaterial != null;
                    obj = componentFactory.InstantiateAsset(processor.Specification);
                }
                else
                {
                    renderer.enabled = false;
                    obj = componentFactory.InstantiateAsset(weapon.Specification);
                }

                obj.transform.SetParent(transform.GetChild(armamentContainer.transform.GetSiblingIndex()), false);
            }
            else
            {
                renderer.enabled = renderConfiguration.MissingComponentMaterial != null;
            }
        }
    }

    private void RenderPlates(Chassis.PlateLocation plateLocation, Chassis chassis, BotAvatar botAvatar, BotRenderConfiguration renderConfiguration)
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
            plateRenderer.enabled = true;

            if(hasPlate)
            {
                JunkyardDogs.Components.Plate plateComponent = plates[i];
                plateComponent.Material.LoadAssetAsync<ScriptableObject>((asset, reference) =>
                {
                    plateRenderer.material = renderConfiguration.Materials.GetAsset(asset);
                }, (e) => { });
            }
            else
            {
                if (renderConfiguration.MissingComponentMaterial != null)
                {
                    plateRenderer.material = renderConfiguration.MissingComponentMaterial;
                }
                else
                {
                    plateRenderer.enabled = false;
                }
            }
        }
    }
}
