using UnityEngine;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ComponentMessageDialog : MessageDialog<ComponentViewModel>
    {
        [SerializeField]
        private ComponentDisplay _componentDisplay;
    
        protected override void Initialize()
        {
            base.Initialize();
            _componentDisplay.RenderComponent((_viewModel as ComponentViewModel).Component);
        }
    }
}