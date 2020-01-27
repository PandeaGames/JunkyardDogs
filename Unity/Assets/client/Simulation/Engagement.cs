﻿using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using System;
using Data;

namespace JunkyardDogs.Simulation
{
    [Serializable]
    public class Engagement
    {
        public SimulationService.Outcome Outcome { get; set; }
        public Arena Arena { get; set; }
        
        [SerializeField]
        private Bot _redCombatent;

        [SerializeField]
        private Bot _blueCombatent;

        [SerializeField]
        private RulesOfEngagement _rules;

        [SerializeField]
        private int _seed;

        private bool _isLoaded;

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public Bot RedCombatent
        {
            get { return _redCombatent; }
            set { _redCombatent = value; }
        }

        public Bot BlueCombatent
        {
            get { return _blueCombatent; }
            set { _blueCombatent = value; }
        }

        public RulesOfEngagement Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

        public int Seed
        {
            get
            {
                return _seed;
            }
            set { _seed = value; }
        }

        public void SetTimeLimit(float timeLimit)
        {
            _rules.MatchTimeLimit = timeLimit;
        }
    }

    public enum Initiator
    {
        BLUE = 0, 
        RED = 1
    };

    [System.Serializable]
    public struct RulesOfEngagement
    {
        public Initiator initiator;
        public float MatchTimeLimit;
    }

}
