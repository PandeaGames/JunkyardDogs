
using System;
using JunkyardDogs.Simulation.Behavior;
using UnityEngine;
using UnityEngine.UI;
using Action = JunkyardDogs.Simulation.Behavior.Action;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ChooseActionDialog : Dialog<ChooseActionDialogViewModel>
    {
        [SerializeField]
        private GameObject _actionDisplaySource;
        [SerializeField] 
        private RectTransform _listContent;
        
        protected override void Initialize()
        {
            _viewModel.ActionList.LoadAssetAsync<ActionList>(OnActionListLoaded, (e) => { });
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
                        _viewModel.Selection = weakReference;
                        Close();
                    });
                }
            }, (e) => { });
            //TODO fill list;
        }
    }
}