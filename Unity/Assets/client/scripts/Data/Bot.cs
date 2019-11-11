using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Agent;
using System;
using Data;
using JunkyardDogs.Components.Gameplay;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Bot : ComponentGrade.IGradedComponent
    {
        [SerializeField] private Motherboard _motherboard;
        [SerializeField] private CPU _CPU;

        [SerializeField]
        private Chassis _chassis;

        [SerializeField] private Agent _agent;
        [SerializeField] private ExpLevel _exp;
        [SerializeField] private Record _record;

        public Motherboard Motherboard
        {
            get => _motherboard;
            set => _motherboard = value;
        }

        public CPU CPU
        {
            get => _CPU;
            set => _CPU = value;
        }
        public Chassis Chassis {
            get => _chassis;
            set => _chassis = value;
         }

        public Agent Agent
        {
            get => _agent;
            set => _agent = value;
        }

        public ExpLevel Exp
        {
            get => _exp;
            set => _exp = value;
        }

        public Record Record
        {
            get => _record;
            set => _record = value;
        }

        public Bot()
        {
            Agent = new Agent();
        }

        public Bot(Chassis chassis) :this()
        {
            Chassis = chassis;
        }
        
        public int GetCPUAttribute(Specifications.CPU.CPUAttribute attribute)
        {
            return CPU.GetAttribute(attribute);
        }

        public Weapon GetArmament(Chassis.ArmamentLocation armamentLocation)
        {
            if (Chassis != null)
                return Chassis.GetArmament(armamentLocation);

            return null;
        }
        
        public WeaponProcessor GetWeaponProcesor(Chassis.ArmamentLocation armamentLocation)
        {
            if (Chassis != null)
                return Chassis.GetWeaponProcessor(armamentLocation);

            return null;
        }

        public int TotalHealth
        {
            get
            {
                int totalHealth = (int) Mathf.Ceil(
                    Chassis.GetSpec<Specifications.Chassis>().BaseHealthMultiplier *
                    (float)Chassis.Material.Data.Density);

                foreach (Plate plate in Chassis.GetAllPlates())
                {
                    totalHealth += (int) plate.Material.Data.Density;
                }
                
                return totalHealth;
            }
        }

        public ComponentGrade Grade
        {
            get
            {
                return ComponentGrade.HighestGrade(Chassis, CPU);
            }
        }
    }
}