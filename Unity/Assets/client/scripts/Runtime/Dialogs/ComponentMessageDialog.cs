using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using System.Collections.Generic;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ComponentMessageDialog : MessageDialog<ComponentViewModel>
    {
        [SerializeField]
        private ComponentDisplay _componentDisplay;
    
        protected override void Initialize()
        {
            base.Initialize();
            _componentDisplay.RenderComponent((_viewModel as ComponentViewModel) .Component);
        }
    }
}