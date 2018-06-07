using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;
using System.Collections.Generic;

public class ComponentMessageDialog : MessageDialog
{
    [Serializable]
    public class ComponentMessageDialogConfig : MessageDialogConfig
    {
        public Component Component;

        public ComponentMessageDialogConfig(List<Option> options) : base(options)
        {
        }
    }

    [SerializeField]
    private ComponentDisplay _componentDisplay;

    private ComponentMessageDialogConfig _config;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        base.Setup(config, responseDelegate);
        _config = config as ComponentMessageDialogConfig;
        _componentDisplay.RenderComponent(_config.Component);
    }
}
