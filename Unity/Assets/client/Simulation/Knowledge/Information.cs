using UnityEngine;
using System.Collections;

namespace JunkyardDogs.Simulation.Knowledge.Information
{
    public struct Information
    {
        public Bot Self;
        public Bot Opponent;
    }

    public struct Bot
    {
        public Vector2 Position;
        public int TotalHealth;
        public int Health;
        public State State;
    }

    public enum State
    {
        AGGRESSIVE, 
        EVASIVE, 
        NATURAL
    }
}
