using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PandeaGames;
using PandeaGames.Data.Static;
using PandeaGames.ViewModels;

public class DialogService : Service
{
    private DialogConfig _config;
    
    [Header("Required")]
    [SerializeField]
    private Transform _stage;

    [Header("Optional")]
    [SerializeField]
    private GameObject _touchBlockerObject;
    private ITouchBlocker _touchBlocker;

    private Dictionary<Type, GameObject> _dialogLookup;

    public override void StartService(ServiceManager serviceManager)
    {
        _dialogLookup = new Dictionary<Type, GameObject>();
        _config = Game.Instance.GetStaticDataPovider<PandeaGameDataProvider>().PandeaGameConfigurationData
            .DialogConfigurationData;

        foreach (GameObject dialogPrefab in _config)
        {
            IDialog dialogComponent = dialogPrefab.GetComponent<IDialog>();
            MessageDialog messageDialogComponent = dialogPrefab.GetComponent<MessageDialog>();

            if (dialogComponent == null)
            {
                Debug.LogError("Missing Dialog component found during service setup");
                continue;
            }

            //if we find a dialog of type MessageDialog, insert it into the dictionary as our generic MessageDialog.
            if(messageDialogComponent)
                _dialogLookup.Add(typeof(MessageDialog), dialogPrefab);

            _dialogLookup.Add(dialogComponent.GetType(), dialogPrefab);
        }

        if (_touchBlockerObject != null)
        {
            _touchBlocker = _touchBlockerObject.GetComponent<ITouchBlocker>();
        }
        
        base.StartService(serviceManager);
    }

    public override void EndService(ServiceManager serviceManager)
    {
        _dialogLookup.Clear();
        _dialogLookup = null;

        base.EndService(serviceManager);
    }

    public void DisplayDialog<T>(IViewModel viewModel) where T:IDialog
    {
        GameObject prefab = GetDialogPrefab<T>();

        if (prefab == null)
        {
            Debug.LogError("Cannot display dialog: " + typeof(T).Name);
            return;
        }

        Debug.Log("Display Dialog type " + typeof(T).Name);
        
        GameObject prefabInstance = Instantiate(prefab, _stage);

        IDialog dialog = prefabInstance.GetComponent<IDialog>();

        if (dialog == null)
        {
            Debug.LogError("Dialog component not found on prefab. Cannot display: " + prefab);
            return;
        }

        dialog.OnCancel += OnDialogCancel;
        dialog.OnClose += OnDialogClose;

        dialog.Setup(viewModel);

        if (_touchBlocker != null)
        {
            InputService.Instance.Enabled = false;
            _touchBlocker.Open(true);
        }
    }

    public GameObject GetDialogPrefab<T>() where T : IDialog
    {
        GameObject prefab;
        _dialogLookup.TryGetValue(typeof(T), out prefab);

        if (prefab == null)
        {
            Debug.LogError("Dialog type not found in service. Please add it to your configuration.");
            return null;
        }

        return prefab;
    }
    
    private void BlurDialog(IDialog dialog)
    {
        dialog.Blur();
    }

    private void OnDialogClose(IDialog dialog)
    {
        BlurDialog(dialog);
        dialog.Destroy();

        if(_touchBlocker != null)
        {
            InputService.Instance.Enabled = true;
            _touchBlocker.Close(true);
        }
    }

    private void OnDialogCancel(IDialog dialog)
    {
        dialog.Destroy();

        if (_touchBlocker != null)
        {
            InputService.Instance.Enabled = true;
            _touchBlocker.Close(true);
        }
    }
}