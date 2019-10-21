using JunkyardDogs.Simulation;
using UnityEngine;

public class SimulationRunnerBehaviour : MonoBehaviour
{
    private SimulatedEngagement _simulatedEngagement;
    private float _lastStep = -1;
    private float delta;
    
    public void Init(SimulatedEngagement simulatedEngagement)
    {
        _simulatedEngagement = simulatedEngagement;
    }

    private void Update()
    {
        if (_simulatedEngagement != null)
        {
            delta += Time.deltaTime;
            while (delta > SimulatedEngagement.SimuationStep)
            {
                delta -= SimulatedEngagement.SimuationStep;
                _lastStep = Time.time;
                if(_simulatedEngagement.Step())
                {
                    _simulatedEngagement = null;
                    Debug.Log("WINNER");
                } 
            }
        }
    }
}
