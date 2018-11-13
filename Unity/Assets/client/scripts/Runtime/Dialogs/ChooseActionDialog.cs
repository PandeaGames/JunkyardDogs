
using System;
using JunkyardDogs.Simulation.Behavior;
using UnityEngine;
using UnityEngine.UI;
using Action = JunkyardDogs.Simulation.Behavior.Action;
using WeakReference = Data.WeakReference;

public class ChooseActionDialog : Dialog
{
    public class ChooseActionDialogConfig : Config
    {
        public WeakReference ActionList;
    }
    
    public class ChooseActionDialogResponse : Response
    {
        public WeakReference Selection;
    }

    [SerializeField]
    private GameObject _actionDisplaySource;

    [SerializeField] private RectTransform _listContent;

    [NonSerialized]
    private ChooseActionDialogConfig _config;

    private WeakReference _selection;
    
    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        base.Setup(config, responseDelegate);
        _config = config as ChooseActionDialogConfig;
        _config.ActionList.LoadAssetAsync<ActionList>(OnActionListLoaded, (e) => { });
    }

    private void OnActionListLoaded(ActionList list, WeakReference reference)
    {
        list.LoadAsync(()=> {
            foreach (var weakReference in list)
            {
                Action action = weakReference.Asset as Action;
                ActionDisplay display = Instantiate(_actionDisplaySource,_listContent, false).GetComponent<ActionDisplay>();
                display.gameObject.SetActive(true);
                display.Render(action);

                Button button = display.GetComponent<Button>();
                
                button.onClick.AddListener(()=>
                {
                    _selection = weakReference;
                    Close();
                });
            }
        }, (e) => { });
        //TODO fill list;
    }

    protected override Response GenerateResponse()
    {
        ChooseActionDialogResponse response = new ChooseActionDialogResponse();
        response.Selection = _selection;
        return response;
    }
}
