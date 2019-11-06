using System.IO;
using Data;
using JunkyardDogs.Data;
using PandeaGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[RequireComponent(typeof(Button))]
public class EventButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField, TournamentStaticDataReference]
    private TournamentStaticDataReference _tournament;

    [SerializeField] 
    private uint _viewModelInstanceId;
    
    [SerializeField]
    private TournamentTimerDisplay _timerDisplay;

    [SerializeField] 
    private Transform _3DTransformLink;
    
    private Button _button;
    private WorldMapViewModel _vm;

    private void Start()
    {
        _vm = Game.Instance.GetViewModel<WorldMapViewModel>(_viewModelInstanceId);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);
        _text.text = Path.GetFileName(_tournament.Data.name);
        OnTournamentLoaded();
    }

    private void Update()
    {
        if (_3DTransformLink != null)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(_3DTransformLink.position);
            transform.position = position;
        }
    }

    private void onClick()
    {
        _vm.TapTournament(_tournament);
    }

    private void OnTournamentLoaded()
    {
        _timerDisplay.Render(_tournament.Data);
    }
}
