using UnityEngine;
using JunkyardDogs.Components;
using System;

public class TakeJunkDialog : Dialog
{
    [Serializable]
    public class TakeJunkDialogResponse : Response
    {
        public GenericComponent Component;
    }
    [Serializable]
    public class TakeJunkDialogConfig : Config
    {
        public GenericComponent Component;
    }

    private TakeJunkDialogConfig _takeJunkDialogConfig = null;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        _takeJunkDialogConfig = config as TakeJunkDialogConfig;

        base.Setup(config, responseDelegate);
    }

    protected override Response GenerateResponse()
    {
        TakeJunkDialogResponse response = new TakeJunkDialogResponse();

        response.Component = _takeJunkDialogConfig.Component;

        return response;
    }
}