using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using System;
using Data;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Chassis : PhysicalComponent
    {
        public enum PlateLocation
        {
            Top,
            Front, 
            Left, 
            Right, 
            Bottom
        }

        public enum ArmamentLocation
        {
            Top,
            Front,
            Left,
            Right
        }

        public List<Plate> FrontPlates { get; set; }

        [SerializeField]
        public List<Plate> LeftPlates { get; set; }

        [SerializeField]
        public List<Plate> RightPlates { get; set; }

        [SerializeField]
        public List<Plate> BackPlates { get; set; }

        [SerializeField]
        public List<Plate> TopPlates { get; set; }

        [SerializeField]
        public List<Plate> BottomPlates { get; set; }

        [SerializeField]
        public WeaponProcessor TopArmament { get; set; }

        [SerializeField]
        public WeaponProcessor FrontArmament { get; set; }

        [SerializeField]
        public WeaponProcessor LeftArmament { get; set; }

        [SerializeField]
        public WeaponProcessor RightArmament { get; set; }

        public Chassis()
        {
            FrontPlates = new List<Plate>();
            LeftPlates = new List<Plate>();
            RightPlates = new List<Plate>();
            BackPlates = new List<Plate>();
            TopPlates = new List<Plate>();
            BottomPlates = new List<Plate>();
        }
        
        public override void Dismantle(Inventory inventory)
        {
            base.Dismantle(inventory);
            
            FrontPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            LeftPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            RightPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            BackPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            TopPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            BottomPlates.ForEach((plate) =>
            {
                inventory.AddComponent(plate);
            });
            
            FrontPlates.Clear();
            LeftPlates.Clear();
            RightPlates.Clear();
            BackPlates.Clear();
            TopPlates.Clear();
            BottomPlates.Clear();

            if (TopArmament != null)
            {
                TopArmament.Dismantle(inventory);
                TopArmament = null;
            }
            
            if (FrontArmament != null)
            {
                FrontArmament.Dismantle(inventory);
                FrontArmament = null;
            }
            
            if (LeftArmament != null)
            {
                LeftArmament.Dismantle(inventory);
                LeftArmament = null;
            }
            
            if (RightArmament != null)
            {
                RightArmament.Dismantle(inventory);
                RightArmament = null;
            }
        }

        public List<Plate> GetPlateList(PlateLocation location)
        {
            switch (location)
            {
                case PlateLocation.Bottom:
                    return BottomPlates;
                case PlateLocation.Front:
                    return FrontPlates;
                case PlateLocation.Left:
                    return LeftPlates;
                case PlateLocation.Right:
                    return RightPlates;
                case PlateLocation.Top:
                    return TopPlates;
                default:
                    return null;
            }
        }

        public Plate RemovePlate(PlateLocation location, int index)
        {
            List<Plate> plateList = GetPlateList(location);

            if (plateList.Count > index)
            {
                plateList[index] = null;
                return plateList[index];
            }

            return null;
        }

        public Plate SetPlate(Plate plate, PlateLocation location, int index)
        {
            List<Plate> plateList = GetPlateList(location);
            Plate removedPlate = null;

            while(plateList.Count <= index)
            {
                plateList.Add(null);
            }

            removedPlate = plateList[index];

            plateList.Insert(index, plate);

            return removedPlate;
        }

        public WeaponProcessor GetWeaponProcessor(ArmamentLocation location)
        {
            switch (location)
            {
                case ArmamentLocation.Front:
                    return FrontArmament;
                case ArmamentLocation.Left:
                    return LeftArmament;
                case ArmamentLocation.Right:
                    return RightArmament;
                case ArmamentLocation.Top:
                    return TopArmament;
                default:
                    return null;
            }
        }
    }
}