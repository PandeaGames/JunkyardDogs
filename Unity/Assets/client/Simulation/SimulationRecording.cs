using System;
using System.Collections.Generic;
using JunkyardDogs.Components;
using UnityEngine;

namespace JunkyardDogs.Simulation
{
    public class SimulationRecording
    {
        public List<List<SimEvent>> Events;
        
        public SimulationRecording()
        {
            Events = new List<List<SimEvent>>();
        }

        public void AddEvent(int step, SimEvent simEvent)
        {
            while (Events.Count <= step)
            {
                Events.Add(new List<SimEvent>());
            }
            
            Events[step].Add(simEvent);
        }
    }
}