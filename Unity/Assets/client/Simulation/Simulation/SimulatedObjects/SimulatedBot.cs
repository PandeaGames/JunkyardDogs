using UnityEngine;
using System.Collections;
using JunkyardDogs.Simulation.Agent;
using JunkyardDogs.Simulation.Knowledge.Information;

namespace JunkyardDogs.Simulation.Simulation
{
    public class SimulatedBot : SimulatedObject
    {
        public event SimuatedObjectDelegate OnBotDestroyed;

        private Bot _bot;

        //TODO get from bot
        public int Health = 100;

        public AgentState State;
        public int DirectiveIndex = 0;
        public Information Information;

        public DelayedAttackAction DelayedAttackAction;
        public double LastMovementCommand;
        public ActionResult MovementAction;

        public Bot Bot { get { return _bot; } }

        public SimulatedBot(Bot bot)
        {
            body = new SimulatedBody();
            collider = new SimulatedCircleCollider(body);
            _bot = bot;
            State = bot.Agent.InitialState;

            ((SimulatedCircleCollider)collider).radius = 0.5f;//TODO: get from chassis
        }

        public void RecieveAttack(SimulatedAttack attack)
        {
            Health -= (int)attack.ActionResult.DamageOuput;
            if (Health <= 0)
            {
                Remove();

                if (OnBotDestroyed != null)
                    OnBotDestroyed(this);
            }
        }
    }
}