//mass is how dense the material is
//volume is how much space it takes up
//weight is mass + volume and gravity shit
//V = (W/G)/D
//W = M & G
//M = D * V

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JunkyardDogs.Simulation.Agent;
using JunkyardDogs.Simulation.Knowledge.Information;
using JunkyardDogs.Simulation.Behavior;
using JunkyardDogs.Components;

namespace JunkyardDogs.Simulation
{
    public class SimulationService : Service
    {
        private const float SimuationStep = (1f / 60f);
        private const double MovementTimeLength = 1;

        private Engagement _engagement;
        private RulesOfEngagement _rules;
        private Outcome _outcome;
        private float _speed = 1f;
        private Coroutine _simulationCoroutine;
        private Bot _redBot, _blueBot;
        private int _ticks;

        private Dictionary<Bot, SimulatedBot> _simulatedBots;

        private List<SimulatedObject> _objectList;
        private List<SimulatedObject> _removeBuffer;

        public override void StartService()
        {
            base.StartService();

            _simulatedBots = new Dictionary<Bot, SimulatedBot>();
            _objectList = new List<SimulatedObject>();
            _removeBuffer = new List<SimulatedObject>();
        }

        public void SetEngagement(Engagement engagement)
        {
            StopSimulation();

            _engagement = engagement;
            _rules = engagement.Rules;

            _simulatedBots.Clear();
            _simulatedBots.Add(engagement.BlueCombatent, new SimulatedBot(engagement.BlueCombatent));
            _simulatedBots.Add(engagement.RedCombatent, new SimulatedBot(engagement.RedCombatent));

            _redBot = _engagement.RedCombatent;
            _blueBot = _engagement.BlueCombatent;

            _simulatedBots[_redBot].OnBotDestroyed += OnBotDestroyed;
            _simulatedBots[_blueBot].OnBotDestroyed += OnBotDestroyed;
        }

        public void StartSimulation()
        {
            StopSimulation();

            _ticks = 0;

            _objectList.Add(_simulatedBots[_redBot]);
            _objectList.Add(_simulatedBots[_blueBot]);

            _simulatedBots[_redBot].position.Set(5, 0);
            _simulatedBots[_blueBot].position.Set(-5, 0);

            _simulationCoroutine = StartCoroutine(SimulationCoroutine());
        }

        public void StopSimulation()
        {
            if (_simulationCoroutine != null)
                StopCoroutine(_simulationCoroutine);
        }

        public void SetSimulationSpeed(float speed)
        {
            _speed = speed;
        }

        private IEnumerator SimulationCoroutine()
        {
            while (_outcome == null)
            {
                //TODO: Figure out how to keep simlulation fixed step, 
                //while also supporting positional interpolation for the sake
                //of messing with the speed of the somulation. 

                yield return new WaitForSeconds(SimuationStep * _speed);
                Step(SimuationStep * _speed);
            }
        }

        private double SimulationTime()
        {
            return _ticks * SimuationStep;
        }

        private void Step(float deltaTime)
        {
            while(_removeBuffer.Count > 0)
            {
                _objectList[0].OnRemove -= RemoveSimulatedObject;
                _objectList.Remove(_removeBuffer[0]);
                _removeBuffer.RemoveAt(0);
            }

            _ticks++;

            SimulateBot(_simulatedBots[_redBot], _simulatedBots[_blueBot]);
            SimulateBot(_simulatedBots[_blueBot], _simulatedBots[_redBot]);
            SimulateEnvironment();
        }

        public void OnDrawGizmos()
        {
            if(_objectList == null)
            {
                return;
            }

            foreach (SimulatedObject simulated in _objectList)
            {
                Gizmos.DrawSphere(simulated.position, simulated.radius);
            }
        }

        private void SimulateBot(SimulatedBot bot, SimulatedBot opponent)
        {
            bot.Information.Self.Health = bot.Health;
            bot.Information.Self.Position = bot.position;
            bot.Information.Self.State = bot.State.State.TargetState;
            bot.Information.Self.Health = bot.Health;

            bot.Information.Opponent.Health = opponent.Health;
            bot.Information.Opponent.Position = opponent.position;
            bot.Information.Opponent.State = opponent.State.State.TargetState;
            bot.Information.Opponent.Health = opponent.Health;

            SimulateStateTransitions(bot, opponent);
            ExecuteDirectives(bot, opponent);
        }

        private void SimulateStateTransitions(SimulatedBot bot, SimulatedBot opponent)
        {
            AgentState state = bot.State;

            List<StateTransition> transitions = state.Transitions;
            StateTransition stateTransition = null;

            foreach (StateTransition transition in transitions)
            {
                List<Knowledge.Knowledge> criteria = transition.Criteria;
                bool isCriteriaMet = true;

                foreach (Knowledge.Knowledge knowledge in criteria)
                {
                    isCriteriaMet &= knowledge.IsTrue(bot.Information);
                }

                if (isCriteriaMet)
                {
                    stateTransition = transition;
                    break;
                }
            }

            if (stateTransition)
            {
                bot.State = stateTransition.StateToTransition;
                bot.DirectiveIndex = 0;
            }
        }

        private void ExecuteDirectives(SimulatedBot bot, SimulatedBot opponent)
        {
            bot.velocity = Vector2.zero;

            if (bot.DelayedAttackAction != null)
            {
                DelayedAttackAction delayedAttackAction = bot.DelayedAttackAction;
                AttackActionResult attackAction = delayedAttackAction.AttackActionResult;
                double endTime = attackAction.ChargeTime + attackAction.RecoveryTime + delayedAttackAction.DelayAttackStart;
                double attackTime = attackAction.ChargeTime + delayedAttackAction.DelayAttackStart;

                if (!delayedAttackAction.HasExecutedDelayedAttack && attackTime < SimulationTime())
                {
                    ExecuteAttack(bot, opponent, attackAction, delayedAttackAction.Processor);
                    delayedAttackAction.HasExecutedDelayedAttack = true;
                }

                if (endTime < SimulationTime())
                {
                    bot.DelayedAttackAction = null;
                }
            }
            else if(bot.LastMovementCommand + MovementTimeLength > SimulationTime())
            {
                bot.velocity = bot.MovementAction.movement * 3;
            }
            else
            {
                AgentState state = bot.State;

                List<Directive> directives = state.Directives;

                if (bot.DirectiveIndex == directives.Count)
                {
                    bot.DirectiveIndex = 0;
                }

                Directive directive = directives[bot.DirectiveIndex];
                ActionResult result = directive.Action.GetResult();

                if (result.attack)
                {
                    ExecuteAttackDirective(bot, opponent, result.weaponSlot);
                }
                else
                {
                    bot.LastMovementCommand = SimulationTime();
                    bot.MovementAction = result;
                }

                bot.DirectiveIndex++;
            }
        }

        private void ExecuteAttackDirective(SimulatedBot simulatedBot, SimulatedBot opponent, WeaponSlot slot)
        {
            Bot bot = simulatedBot.Bot;
            Chassis chassis = bot.Chassis;
            WeaponProcessor processor = null;

            switch(slot)
            {
                case WeaponSlot.RIGHT:
                    processor = chassis.RightArmament;
                    break;
                case WeaponSlot.LEFT:
                    processor = chassis.LeftArmament;
                    break;
                case WeaponSlot.TOP:
                    processor = chassis.TopArmament;
                    break;
                case WeaponSlot.FRONT:
                    processor = chassis.FrontArmament;
                    break;
            }

            if (processor == null)
                return;

            Weapon weapon = processor.Weapon;

            if (weapon == null)
                return;

            Specifications.Weapon specification = weapon.Specification;

            AttackActionResult attackAction = specification.GetResult();

            DelayedAttackAction delayedAttackAction = new DelayedAttackAction();

            delayedAttackAction.DelayAttackStart = SimulationTime();
            delayedAttackAction.AttackActionResult = attackAction;
            delayedAttackAction.Processor = processor;

            simulatedBot.DelayedAttackAction = delayedAttackAction;
        }

        private void ExecuteAttack(SimulatedBot simulatedBot, SimulatedBot opponent, AttackActionResult attackAction, WeaponProcessor processor)
        {
            SimulatedAttack simulatedAttack = null;

            switch (attackAction.Type)
            {
                case AttackActionResultType.HITSCAN:
                    simulatedAttack = new SimulatedHitscan(simulatedBot, opponent, attackAction, processor);
                    break;
                case AttackActionResultType.MELEE:
                    simulatedAttack = new SimulatedMelee(simulatedBot, opponent, attackAction, processor);
                    break;
                case AttackActionResultType.MORTAR:
                    simulatedAttack = new SimulatedMortar(simulatedBot, opponent, attackAction, processor);
                    break;
                case AttackActionResultType.PROJECTILE:
                    simulatedAttack = new SimulatedProjectile(simulatedBot, opponent, attackAction, processor);
                    break;
                case AttackActionResultType.PULSE:
                    simulatedAttack = new SimulatedPulse(simulatedBot, opponent, attackAction, processor);
                    break;
            }

            //TODO add simlulated attack to a list. And then simulate it in the environment. 
            AddSimulatedObject(simulatedAttack);
        }

        private void RemoveSimulatedObject(SimulatedObject simulated)
        {
            _removeBuffer.Add(simulated);
        }

        private void AddSimulatedObject(SimulatedObject simulated)
        {
            _objectList.Add(simulated);
            simulated.OnRemove += RemoveSimulatedObject;
        }

        private void SimulateEnvironment()
        {
            //Update positions
            foreach(SimulatedObject simulated in _objectList)
            {
                simulated.position += simulated.velocity * SimuationStep;
            }

            //Calculate collisions
            for (int i = 0; i<_objectList.Count - 1;i++)
            {
                SimulatedObject simulated = _objectList[i];

                for (int j = i + 1; j < _objectList.Count; j++)
                {
                    SimulatedObject other = _objectList[j];

                    if (Vector2.Distance(simulated.position, other.position) < simulated.radius + other.radius)
                    {
                        simulated.OnCollide(other);
                        other.OnCollide(simulated);
                    }
                }
            }
        }

        private void OnBotDestroyed(SimulatedObject bot)
        {
            SimulatedBot simulatedBot = bot as SimulatedBot;

            if(simulatedBot != null)
            {
                Bot winner = simulatedBot.Bot;
                Bot loser = _redBot == winner ? _blueBot : _redBot;

                this._outcome = new Outcome(winner, loser);
            }
        }

        public class Outcome
        {
            private Bot _winner;
            private Bot _loser;

            public Outcome(Bot winner, Bot loser)
            {
                _winner = winner;
                _loser = loser;
            }
        }

        private class DelayedAttackAction
        {
            public bool WaitForAttack;
            public double DelayAttackStart;
            public bool HasExecutedDelayedAttack;
            public AttackActionResult AttackActionResult;
            public WeaponProcessor Processor;
        }

        private class SimulatedObject
        {
            public delegate void SimuatedObjectDelegate(SimulatedObject simulated);

            public event SimuatedObjectDelegate OnRemove;

            public Vector2 position;
            public Vector2 velocity;
            public float radius;

            public virtual void OnCollide(SimulatedObject other)
            {

            }

            protected void Remove()
            {
                if (OnRemove != null)
                    OnRemove(this);
            }
        }

        private class SimulatedAttack : SimulatedObject
        {
            private SimulatedBot _bot;
            private SimulatedBot _opponent;
            private WeaponProcessor _processor;
            private AttackActionResult _actionResult;

            public SimulatedBot Bot { get { return _bot; } }
            public WeaponProcessor Processor { get { return _processor; } }
            public AttackActionResult ActionResult { get { return _actionResult; } }

            public SimulatedAttack(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor)
            {
                _bot = bot;
                _opponent = opponent;
                _actionResult = actionResult;
                _processor = processor;
            }

            public override void OnCollide(SimulatedObject other)
            {
                SimulatedBot simulatedBot = other as SimulatedBot;

                if (simulatedBot != null && other == _opponent)
                {
                    simulatedBot.RecieveAttack(this);
                    Remove();
                }
            }
        }

        private class SimulatedBot : SimulatedObject
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
                _bot = bot;
                State = bot.Agent.InitialState;
                radius = 0.5f;//TODO: get from chassis
            }

            public void RecieveAttack(SimulatedAttack attack)
            {
                Health -= (int)attack.ActionResult.DamageOuput;
                if(Health<=0)
                {
                    Remove();

                    if (OnBotDestroyed != null)
                        OnBotDestroyed(this);
                }
            }
        }

        private class SimulatedProjectile: SimulatedAttack
        {
            public SimulatedProjectile(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
            {
                Vector2 delta =  opponent.position - bot.position;

                position = bot.position;
                radius = actionResult.ProjectileResult.Radius;

                float angle = Mathf.Atan2(delta.y, delta.x);

                velocity.Set(
                    Mathf.Cos(angle) * actionResult.ProjectileResult.Velocity,
                    Mathf.Sin(angle) * actionResult.ProjectileResult.Velocity
                    );
            }
        }

        private class SimulatedMelee : SimulatedAttack
        {
            public SimulatedMelee(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
            {
            }
        }

        private class SimulatedHitscan : SimulatedAttack
        {
            public SimulatedHitscan(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
            {
            }
        }

        private class SimulatedMortar : SimulatedAttack
        {
            public SimulatedMortar(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor) : base(bot, opponent, actionResult, processor)
            {
            }
        }

        private class SimulatedPulse : SimulatedAttack
        {
            public float PulseVelocity = 0;

            public SimulatedPulse(SimulatedBot bot, SimulatedBot opponent, AttackActionResult actionResult, WeaponProcessor processor):base(bot, opponent, actionResult, processor)
            {
                PulseVelocity = actionResult.PulseResult.Velocity;
            }
        }
    }
    public struct ActionResult
    {
        public Vector2 movement;
        public bool attack;
        public WeaponSlot weaponSlot;
    }

    public enum AttackActionResultType
    {
        MELEE,
        PROJECTILE,
        MORTAR,
        HITSCAN,
        PULSE
    }

    public struct AttackActionResult
    {
        public float DamageOuput;
        public double ChargeTime;
        public double RecoveryTime;
        public AttackActionResultType Type;

        public struct Melee
        {
            public float range;
        }

        public struct Hitscan
        {
            public float Thickness;
        }

        public struct Projectile
        {
            public float Velocity;
            public float Radius;
            public float Range;
        }

        public struct Pulse
        {
            public float Velocity;
            public float Range;
        }

        public struct Mortar
        {
            public float Velocity;
            public float Radius;
        }

        public Melee MeleeResult;
        public Pulse PulseResult;
        public Hitscan HitspanResult;
        public Projectile ProjectileResult;
        public Mortar MortarResult;
    }
}