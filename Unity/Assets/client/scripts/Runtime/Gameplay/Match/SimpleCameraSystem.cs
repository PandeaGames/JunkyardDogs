using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation;
using PandeaGames;
using UnityEngine;

public class SimpleCameraSystem : MonoBehaviour, ISimulatedEngagementEventHandler
{
    public float CameraTime;
    public GameObject followCameraAgentPrefab;
    public CameraMultiTarget CameraMultiTarget;
    public CameraAgent StaticCameraAgent;
    private List<GameObject> targets = new List<GameObject>();

    private float _lastCameraSwitchTime;
    private int _currentCameraIndex;
    private CameraViewModel _cameraViewModel;
    

    private List<CameraAgent> _cameraAgents = new List<CameraAgent>();

    private List<BotFollowCameraMonoView> _botFollowAgents = new List<BotFollowCameraMonoView>();
    private List<SimBot> _simBots = new List<SimBot>();
    // Start is called before the first frame update
    void Start()
    {
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        _cameraAgents = new List<CameraAgent>();
        _cameraAgents.Add(CameraMultiTarget.gameObject.GetComponent<CameraAgent>());
        //_cameraAgents.Add(StaticCameraAgent);
        followCameraAgentPrefab.SetActive(false);
        _lastCameraSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMultiTarget.Yaw += 0.22f;

        if (Time.time > _lastCameraSwitchTime + CameraTime)
        {
            _lastCameraSwitchTime = Time.time;
            _currentCameraIndex++;

            if (_currentCameraIndex >= _cameraAgents.Count)
            {
                _currentCameraIndex = 0;
            }
            
            //TODO: Make a camera system that works
            //_cameraViewModel.Focus(_cameraAgents[_currentCameraIndex]);
        }
    }
    
    
    public void OnSimEvent(SimulatedEngagement engagement, SimEvent simEvent)
    {
        if (simEvent is SimInstantiationEvent)
        {
            OnSimEvent(engagement, simEvent as SimInstantiationEvent);
        }
    }

    public Type[] EventsToHandle()
    {
        throw new NotImplementedException();
    }

    private void OnSimEvent(SimulatedEngagement engagement, SimInstantiationEvent simEvent)
    {
        if (simEvent.instance is SimBot)
        {
            SimBot simBot = simEvent.instance as SimBot;
            _simBots.Add(simBot);
            GameObject followTargetGO = new GameObject("SimBotCameraTarget");
            SimTargetView targetView = followTargetGO.AddComponent<SimTargetView>();
            targetView.Follow(simBot);

            GameObject followCameraObject = Instantiate(followCameraAgentPrefab);
            followCameraObject.SetActive(true);
            SimpleFollowAgent followAgent = followCameraObject.GetComponent<SimpleFollowAgent>();
            followAgent.SetTarget(targetView.transform);
            
            //_cameraAgents.Add(followAgent);
            targets.Add(followTargetGO);
            
            GameObject personalFollowCamera = new GameObject("PersonalFolloowCameraAgent");
            BotFollowCameraMonoView botFollowCameraMonoView = personalFollowCamera.AddComponent<BotFollowCameraMonoView>();
            _botFollowAgents.Add(botFollowCameraMonoView);

            if (_botFollowAgents.Count > 1)
            {
                _botFollowAgents[0].Setup(_simBots[0], _simBots[1]);
                _botFollowAgents[1].Setup(_simBots[1], _simBots[0]);
                _cameraViewModel.Focus(_botFollowAgents[0]);
            }
        }
        
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i--);
            }
        }
        CameraMultiTarget.SetTargets(targets.ToArray());
    }
}
