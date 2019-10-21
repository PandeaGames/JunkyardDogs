using System;
using JunkyardDogs.Simulation;
using TMPro;
using UnityEngine;

public class MatchUIBehaviour : MonoBehaviour, ISimulatedEngagementEventHandler
{
    [SerializeField] 
    private BotStatusBehaviour _botStatusBehaviourSource;

    [SerializeField] 
    private RectTransform _botStatusContainer;

    [SerializeField] 
    private MatchTimerUI _matchTimerUI;

    private SimulatedEngagement _simulatedEngagement;
    
    public void Setup(SimulatedEngagement simulatedEngagement)
    {
        _simulatedEngagement = simulatedEngagement;
        simulatedEngagement.AddEventHandler(this);
        _matchTimerUI.Setup(simulatedEngagement);
    }

    private void OnDestroy()
    {
        if (_simulatedEngagement != null)
        {
            _simulatedEngagement.RemoveEventHandler(this);
            _simulatedEngagement = null;
        }
    }

    public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
    {
        if (simEvent is SimInstantiationEvent)
        {
            OnSimEvent(engagement, simEvent as SimInstantiationEvent);
        }
    }
    
    public void OnSimEvent(SimulatedEngagement engagement, SimInstantiationEvent simEvent)
    {
        if (simEvent.instance is SimBot)
        {
            AddSimBot(simEvent.instance as SimBot);
        }
    }
    
    public void AddSimBot(SimBot simBot)
    {
        GameObject botStatus = Instantiate(_botStatusBehaviourSource.gameObject, _botStatusContainer, worldPositionStays: false);
        BotStatusBehaviour botStatusBehaviour = botStatus.GetComponent<BotStatusBehaviour>();
        botStatusBehaviour.Setup(simBot);
    }

    public Type[] EventsToHandle()
    {
        return new Type[] { typeof(SimInstantiationEvent) };
    }
}
