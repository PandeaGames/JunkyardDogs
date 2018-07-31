using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation.Agent;
using System;

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

        }

        public Bot(Chassis chassis)
        {
            Chassis = chassis;
        }

        public void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            int objectsToLoad = 0;
            bool hasError = false;

            Action onInternalLoadSuccess = () =>
            {
                if(--objectsToLoad <= 0)
                {
                    if(hasError)
                    {
                        onLoadFailed();
                    }
                    else
                    {
                        onLoadSuccess();
                    }
                }
            };

            Action onInternalLoadError = () =>
            {
                hasError = true;

                if (--objectsToLoad <= 0)
                {
                    onLoadFailed();
                }
            };

            if (Motherboard != null)
            {
                objectsToLoad++;
                Motherboard.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
            }

            if (Chassis != null)
            {
                objectsToLoad++;
                Chassis.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
            }

            if (Agent != null)
            {
                objectsToLoad++;
                Agent.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
            }
        }

        public bool IsLoaded()
        {
            return _isLoaded;
        }
    }
}