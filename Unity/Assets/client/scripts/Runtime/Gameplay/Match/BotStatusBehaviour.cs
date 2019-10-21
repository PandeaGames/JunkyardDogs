using JunkyardDogs.Simulation;
using TMPro;
using UnityEngine;

public class BotStatusBehaviour : MonoBehaviour
{
    private SimBot _simObject;

    [SerializeField]
    private RTProgressBarBehaviour _healthProgressBar;

    [SerializeField] private TMP_Text _healthText;
    
    public void Setup(SimBot simObject)
    {
        _simObject = simObject;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_simObject != null)
        {
            _healthProgressBar.SetProgress((float) _simObject.RemainingHealth / (float)_simObject.TotalHealth);
            _healthText.text = ((int)_simObject.RemainingHealth).ToString();
        }
    }
}
