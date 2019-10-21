using JunkyardDogs.Simulation;
using TMPro;
using UnityEngine;

public class MatchTimerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private SimulatedEngagement _engagement;
    
    public void Setup(SimulatedEngagement engagement)
    {
        _engagement = engagement;
    }

    private void Update()
    {
        if (_engagement != null)
        {
            double seconds = _engagement.CurrentSeconds;
            seconds *= 10;
            int secondsInt = (int) seconds;
            float secondsFloat = secondsInt / 10;
            _text.text = secondsFloat.ToString();
        }
    }
}
