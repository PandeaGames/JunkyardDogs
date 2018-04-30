using UnityEngine;
using JunkyardDogs.Components;
using System;
using UnityEngine.UI;
using Component = JunkyardDogs.Components.Component;
using TMPro;

public class TakeJunkDialog : Dialog
{
    [Serializable]
    public class TakeJunkDialogResponse : Response
    {
        public Component Component;
    }
    [Serializable]
    public class TakeJunkDialogConfig : Config
    {
        public Component Component;
    }

    [SerializeField]
    private SpriteFactory _spriteFactory;

    [SerializeField]
    private Image _componentIcon;

    [SerializeField]
    private TMP_Text _componentText;

    private TakeJunkDialogConfig _takeJunkDialogConfig = null;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        _takeJunkDialogConfig = config as TakeJunkDialogConfig;

        _takeJunkDialogConfig.Component.SpecificationReference.LoadAsync<ScriptableObject>(
            (asset, reference) => 
            {
                _componentIcon.sprite = _spriteFactory.GetAsset(asset);
                _componentText.text = asset.name;
            },
            null
            );

        base.Setup(config, responseDelegate);
    }

    protected override Response GenerateResponse()
    {
        TakeJunkDialogResponse response = new TakeJunkDialogResponse();

        response.Component = _takeJunkDialogConfig.Component;

        return response;
    }
}