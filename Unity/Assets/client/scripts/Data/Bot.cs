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
    public class Bot:ILoadableObject
    {
        public Motherboard Motherboard { get; set; }
        public Chassis Chassis { get; set; }
        public Agent Agent { get; set; }

        private bool _isLoaded;

        public Bot()
        {
            Agent = new Agent();
        }

        public Bot(Chassis chassis) :this()
        {
            Chassis = chassis;
        }

        public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();

            loader.AppendProvider(Motherboard);
            loader.AppendProvider(Chassis);
            loader.AppendProvider(Agent);
            
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }

        public bool IsLoaded()
        {
            return _isLoaded;
        }
    }
}