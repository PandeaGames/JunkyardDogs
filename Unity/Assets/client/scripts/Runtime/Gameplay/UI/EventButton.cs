using System.IO;
using Data;
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

    [SerializeField, WeakReference(typeof(Tournament))]
    private WeakReference _tournament;

    [SerializeField] 
    private uint _viewModelInstanceId;
    
    private Button _button;
    private WorldMapViewModel _vm;

    private void Start()
    {
        _vm = Game.Instance.GetViewModel<WorldMapViewModel>(_viewModelInstanceId);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);
        _text.text = Path.GetFileName(_tournament.Path);
    }

    private void onClick()
    {
        _vm.TapTournament(_tournament);
    }
}
