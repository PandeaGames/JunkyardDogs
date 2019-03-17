using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Agent;
using System;
using Data;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Bot
    {
        public Motherboard Motherboard { get; set; }
        public Chassis Chassis { get; set; }
        public Agent Agent { get; set; }

        public Bot()
        {
            Agent = new Agent();
        }

        public Bot(Chassis chassis) :this()
        {
            Chassis = chassis;
        }
    }
}