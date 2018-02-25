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
using JunkyardDogs.Simulation.Simulation;

namespace JunkyardDogs.Simulation
{
    public class SimulationService : Service
    {
        public const float SimuationStep = 1f / 60f;
        private float VariableSimulationStep { get { return 1f / (60f * _speed); } }
        private const double MovementTimeLength = 1;

        private Engagement _engagement;
        private RulesOfEngagement _rules;
        private Outcome _outcome;

        [SerializeField][Range(0, 1f)]
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

            _simulatedBots[_redBot].body.position.Set(5, 0);
            _simulatedBots[_blueBot].body.position.Set(-5, 0);

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
                // Debug.Log(SimuationStep / _speed);

                if (VariableSimulationStep >= SimuationStep)
                {
                    yield return new WaitForSeconds(VariableSimulationStep);
                }
                
                //yield return new WaitForSecondsRealtime
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
                if (simulated.collider == null)
                    continue;

                simulated.collider.OnDrawGizmos();
            }
        }

        private void SimulateBot(SimulatedBot bot, SimulatedBot opponent)
        {
            bot.Information.Self.Health = bot.Health;
            bot.Information.Self.Position = bot.body.position;
            bot.Information.Self.State = bot.State.State.TargetState;
            bot.Information.Self.Health = bot.Health;

            bot.Information.Opponent.Health = opponent.Health;
            bot.Information.Opponent.Position = opponent.body.position;
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
            bot.body.velocity = Vector2.zero;

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
                //TODO: get 3 from the speed of the chassis. based on weight and locomotion
                //bot.body.velocity = bot.MovementAction.movement * 3;

                Vector2 delta = opponent.body.position - bot.body.position;
                Vector2 moveDelta = bot.MovementAction.movement;

                float angle = Mathf.Atan2(delta.y, delta.x);
                float moveAngle = Mathf.Atan2(moveDelta.x, moveDelta.y);

                float finalAngle = moveAngle + angle;

                bot.body.velocity.Set(
                    Mathf.Cos(finalAngle) * 3,
                    Mathf.Sin(finalAngle) * 3
                    );
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
                if(simulated.body != null)
                    simulated.body.position += simulated.body.velocity * SimuationStep;

                simulated.Update();
            }

            //Calculate collisions
            for (int i = 0; i<_objectList.Count - 1;i++)
            {
                SimulatedObject simulated = _objectList[i];

                if (simulated.collider == null)
                    continue;

                for (int j = i + 1; j < _objectList.Count; j++)
                {
                    SimulatedObject other = _objectList[j];

                    if (other.collider == null)
                        continue;
                         
                    if(DoesCollide(simulated.collider, other.collider))
                    {
                        simulated.OnCollide(other);
                        other.OnCollide(simulated);
                    }
                }
            }
        }

        private bool DoesCollide(SimulatedCollider collider, SimulatedCollider other)
        {
            SimulatedCircleCollider colliderCircle = collider as SimulatedCircleCollider;
            SimulatedCircleCollider otherCircle = other as SimulatedCircleCollider;

            SimulatedLineCollider colliderLine = collider as SimulatedLineCollider;
            SimulatedLineCollider otherLine = other as SimulatedLineCollider;

            if (colliderCircle != null && otherCircle != null)
            {
                if (Vector2.Distance(colliderCircle.Body.position, otherCircle.Body.position) < colliderCircle.radius + otherCircle.radius)
                {
                    return true;
                }
            }

            if(colliderLine != null && otherLine != null)
            {
                return false;
            }

            if (colliderLine != null || otherLine != null)
            {
                SimulatedLineCollider line = colliderLine == null ? otherLine : colliderLine;
                SimulatedCircleCollider circle = colliderLine == null ? colliderCircle : otherCircle;

                Vector2 p1 = line.Body.position;
                Vector2 p2 = line.Body.position;

                p2.x += Mathf.Cos(line.angle) * 10;
                p2.y += Mathf.Sin(line.angle) * 10;

                Vector2 c = circle.Body.position;
                float r = circle.radius;

                Vector2 p3 = new Vector2(p1.x - c.x, p1.y - c.y);
                Vector2 p4 = new Vector2(p2.x - c.x, p2.y - c.y);
                //var p3 = { x:p1.x - c.x, y: p1.y - c.y}; //shifted line points
                //var p4 = { x:p2.x - c.x, y: p2.y - c.y};

                float m = (p4.y - p3.y) / (p4.x - p3.x); //slope of the line
                float b = p3.y - m * p3.x; //y-intercept of line

                float underRadical = Mathf.Pow(r, 2) * Mathf.Pow(m, 2) + Mathf.Pow(r, 2) - Mathf.Pow(b, 2); //the value under the square root sign 

                if (underRadical< 0) {
                    //line completely missed
                    return false;
                } else {
                    float t1 = (-m * b + Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //one of the intercept x's
                    float t2 = (-m * b - Mathf.Sqrt(underRadical)) / (Mathf.Pow(m, 2) + 1); //other intercept's x
                    Vector2 i1 = new Vector2(t1 + c.x, m * t1 + b + c.y);
                    Vector2 i2 = new Vector2(t2 + c.x, m * t2 + b + c.y);
                   // var i1 = { x:t1 + c.x, y:m * t1 + b + c.y }; //intercept point 1
                    //var i2 = { x:t2 + c.x, y:m * t2 + b + c.y }; //intercept point 2
                    return true;
                }
            }

            return false;
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

        
    }
    public class DelayedAttackAction
    {
        public bool WaitForAttack;
        public double DelayAttackStart;
        public bool HasExecutedDelayedAttack;
        public AttackActionResult AttackActionResult;
        public WeaponProcessor Processor;
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
 