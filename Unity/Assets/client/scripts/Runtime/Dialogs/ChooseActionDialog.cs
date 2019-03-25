
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation.Behavior;
using PandeaGames;
using UnityEngine;
using UnityEngine.UI;
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
            foreach (var weakReference in Game.Instance.GetStaticDataPovider<ActionStaticDataProvider>())
            {
                BehaviorAction behaviorAction = weakReference.Data;
                ActionDisplay display = Instantiate(_actionDisplaySource,_listContent, false).GetComponent<ActionDisplay>();
                display.gameObject.SetActive(true);
                display.Render(behaviorAction);
    
                Button button = display.GetComponent<Button>();
                    
                button.onClick.AddListener(()=>
                {
                    _viewModel.Selection = weakReference;
                    Close();
                });
            }
        }
    }
}