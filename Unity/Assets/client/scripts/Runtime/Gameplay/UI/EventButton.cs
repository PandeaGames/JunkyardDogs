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


    private TournamentService _tournamentService;
    
    private Button _button;
    private WorldMapViewModel _vm;

    private void Start()
    {
        _tournamentService = Game.Instance.GetService<TournamentService>();
        _vm = Game.Instance.GetViewModel<WorldMapViewModel>(_viewModelInstanceId);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);
        _text.text = Path.GetFileName(_tournament.Data.name);
    }

    private void Update()
    {
        _timerDisplay.Render(_tournament.Data);
        TournamentStatus status = _vm.User.GetTournamentStatus(_tournament);
        _button.interactable = status != TournamentStatus.Locked;
    }

    private void onClick()
    {
        _vm.TapTournament(_tournament);
    }
}
