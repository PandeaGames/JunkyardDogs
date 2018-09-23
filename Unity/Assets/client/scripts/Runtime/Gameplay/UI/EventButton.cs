using System.IO;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EventButton : MonoBehaviour
{
    [SerializeField]
    private ServiceManager _serviceManager;
    
    [SerializeField]
    private TMP_Text _text;

    [SerializeField, WeakReference(typeof(Tournament))]
    private WeakReference _tournament;
    
    private Button _button;
    private DialogService _dialogSevice;

    private void Start()
    {
        _dialogSevice = _serviceManager.GetService<DialogService>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClick);
        _text.text = Path.GetFileName(_tournament.Path);
    }

    private void onClick()
    {
        var config = ScriptableObject.CreateInstance<EventDialog.EventDialogConfig>();

        config.ServiceManager = _serviceManager;
        config.Tournament = _tournament;
        
        _dialogSevice.DisplayDialog<EventDialog>(config);
    }
}
