using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Chassis : Component
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

        }

        private List<Plate> GetPlateList(PlateLocation location)
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

            if(plateList.Count > index)
            {
                removedPlate = plateList[index];
            }
            else
            {
                plateList.Capacity = index + 1;
            }

            plateList[index] = plate;

            return removedPlate;
        }
    }
}