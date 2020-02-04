using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;
using PandeaGames;
using Chassis = JunkyardDogs.Components.Chassis;
using Material = UnityEngine.Material;

public class BotRenderer : MonoBehaviour
{
    public void Render(Bot bot, BotRenderConfiguration renderConfiguration)
    {
        BotAvatar botAvatar = renderConfiguration.AvatarFactory.GetAsset(bot.Chassis.Specification) as BotAvatar;
        botAvatar.ApplyAvatar(this.gameObject);
        Chassis chassis = bot.Chassis;

        RenderPlates(Chassis.PlateLocation.Bottom, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Front, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Left, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Right, chassis, botAvatar, renderConfiguration);
        RenderPlates(Chassis.PlateLocation.Top, chassis, botAvatar, renderConfiguration);

        RenderArmament(bot, Chassis.ArmamentLocation.Front, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Left, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Right, chassis, botAvatar, renderConfiguration);
        RenderArmament(bot, Chassis.ArmamentLocation.Top, chassis, botAvatar, renderConfiguration);

        Renderer renderer = transform.GetChild(botAvatar.Frame.transform.GetSiblingIndex()).GetComponent<Renderer>();
        renderer.material = renderConfiguration.Materials.GetAsset(bot.Chassis.Material.Data);
    }

    private void RenderArmament(Bot bot, Chassis.ArmamentLocation location, Chassis chasiss, BotAvatar botAvatar, BotRenderConfiguration renderConfiguration)
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

                Transform parent = transform.GetChild(armamentContainer.transform.GetSiblingIndex()).transform;
                GameObject prefab;
                if(weapon == null)
                {
                    renderer.enabled = renderConfiguration.MissingComponentMaterial != null;
                    prefab = SynchronousStaticDataProvider.Instance.GetData(processor).Prefab;
                }
                else
                {
                    renderer.enabled = false;
                    prefab = SynchronousStaticDataProvider.Instance.GetData(weapon).Prefab;
                }

                if (prefab != null)
                {
                    Instantiate(prefab, parent, worldPositionStays:false);
                }
                else
                {
                    Debug.LogWarning($"Unable to render armament {location}");
                }
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
                plateRenderer.material = renderConfiguration.Materials.GetAsset(plateComponent.Material.Data);
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
