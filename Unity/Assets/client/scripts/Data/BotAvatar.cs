using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;

[CreateAssetMenu]
public class BotAvatar : ScriptableObject
{
    public enum AvatarComponent
    {
        Frame, 
        Plate, 
        Armament
    }

    public GameObject Frame;

    public List<GameObject> FrontPlates;

    [SerializeField]
    public List<GameObject> LeftPlates;

    [SerializeField]
    public List<GameObject> RightPlates;

    [SerializeField]
    public List<GameObject> BackPlates;

    [SerializeField]
    public List<GameObject> TopPlates;

    [SerializeField]
    public List<GameObject> BottomPlates;

    [SerializeField]
    public GameObject TopArmament;

    [SerializeField]
    public GameObject FrontArmament;

    [SerializeField]
    public GameObject LeftArmament;

    [SerializeField]
    public GameObject RightArmament;

    public AvatarComponent GetAvatarComponent(GameObject component)
    {
        return GetAvatarComponent(component.transform.GetSiblingIndex());
    }

    public AvatarComponent GetAvatarComponent(int index)
    {
        if (FrontPlates.Find((avatarComponent) => avatarComponent.transform.GetSiblingIndex() == index) || 
            LeftPlates.Find((avatarComponent) => avatarComponent.transform.GetSiblingIndex() == index) ||
            RightPlates.Find((avatarComponent) => avatarComponent.transform.GetSiblingIndex() == index) || 
            TopPlates.Find((avatarComponent) => avatarComponent.transform.GetSiblingIndex() == index) ||
            BottomPlates.Find((avatarComponent) => avatarComponent.transform.GetSiblingIndex() == index))
        {
            return AvatarComponent.Plate;
        }
        else if(TopArmament && TopArmament.transform.GetSiblingIndex() == index ||
            FrontArmament && FrontArmament.transform.GetSiblingIndex() == index ||
            LeftArmament && LeftArmament.transform.GetSiblingIndex() == index ||
            RightArmament && RightArmament.transform.GetSiblingIndex() == index)
        {
            return AvatarComponent.Armament;
        }

        return AvatarComponent.Plate;
    }

    public Chassis.ArmamentLocation GetArmamentLocation(GameObject armament)
    {
        return GetArmamentLocation(armament.transform);
    }

    public Chassis.ArmamentLocation GetArmamentLocation(Transform armament)
    {
        return GetArmamentLocation(armament.GetSiblingIndex());
    }

    public Chassis.ArmamentLocation GetArmamentLocation(int armament)
    {
        if(TopArmament && TopArmament.transform.GetSiblingIndex() == armament)
        {
            return Chassis.ArmamentLocation.Top;
        }
        else if (FrontArmament && FrontArmament.transform.GetSiblingIndex() == armament)
        {
            return Chassis.ArmamentLocation.Front;
        }
        else if (LeftArmament && LeftArmament.transform.GetSiblingIndex() == armament)
        {
            return Chassis.ArmamentLocation.Left;
        }
        else if (RightArmament && RightArmament.transform.GetSiblingIndex() == armament)
        {
            return Chassis.ArmamentLocation.Right;
        }

        return default(Chassis.ArmamentLocation);
    }

    public GameObject GetArmamentContainer(Chassis.ArmamentLocation location)
    {
        switch(location)
        {
            case Chassis.ArmamentLocation.Front:
                return FrontArmament;
            case Chassis.ArmamentLocation.Left:
                return LeftArmament;
            case Chassis.ArmamentLocation.Right:
                return RightArmament;
            case Chassis.ArmamentLocation.Top:
                return TopArmament;
        }

        return null;
    }

    public Chassis.PlateLocation GetPlateLocation(GameObject plate)
    {
        return GetPlateLocation(plate.transform);
    }

    public Chassis.PlateLocation GetPlateLocation(Transform plate)
    {
        return GetPlateLocation(plate.GetSiblingIndex());
    }

    public Chassis.PlateLocation GetPlateLocation(int plate)
    {
        if (FrontPlates.Find((plateAvatarComponent) => plateAvatarComponent.transform.GetSiblingIndex() == plate))
        {
            return Chassis.PlateLocation.Front;
        }
        else if(LeftPlates.Find((plateAvatarComponent) => plateAvatarComponent.transform.GetSiblingIndex() == plate))
        {
            return Chassis.PlateLocation.Left;
        }
        else if (RightPlates.Find((plateAvatarComponent) => plateAvatarComponent.transform.GetSiblingIndex() == plate))
        {
            return Chassis.PlateLocation.Right;
        }
        else if (TopPlates.Find((plateAvatarComponent) => plateAvatarComponent.transform.GetSiblingIndex() == plate))
        {
            return Chassis.PlateLocation.Top;
        }
        else
        {
            return Chassis.PlateLocation.Bottom;
        }
    }

    public int GetPlateIndex(GameObject plate)
    {
        Chassis.PlateLocation location = GetPlateLocation(plate);
        List<GameObject> plateList = GetPlateList(location);

        for(int i = 0; i < plateList.Count; i++)
        {
            if(plateList[i].transform.GetSiblingIndex() == plate.transform.GetSiblingIndex())
            {
                return i;
            }
        }

        return -1;
    }

    public List<GameObject> GetPlateList(Chassis.PlateLocation location)
    {
        switch (location)
        {
            case Chassis.PlateLocation.Bottom:
                return BottomPlates;
            case Chassis.PlateLocation.Front:
                return FrontPlates;
            case Chassis.PlateLocation.Left:
                return LeftPlates;
            case Chassis.PlateLocation.Right:
                return RightPlates;
            case Chassis.PlateLocation.Top:
                return TopPlates;
            default:
                return null;
        }
    }
}
