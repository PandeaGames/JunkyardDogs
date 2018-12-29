using UnityEngine;
using JunkyardDogs.Components;
using System;
using JunkyardDogs.scripts.Runtime.Dialogs;
using UnityEngine.UI;
using Component = JunkyardDogs.Components.Component;
using TMPro;

public class TakeJunkDialog : Dialog<TakeJunkDialogViewModel>
{
    [SerializeField]
    private SpriteFactory _spriteFactory;

    [SerializeField]
    private Image _componentIcon;

    [SerializeField]
    private TMP_Text _componentText;

    [SerializeField]
    private Button _takeJunkButton;

    protected override void Initialize()
    {
        _viewModel.ModelData.Component.SpecificationReference.LoadAssetAsync<ScriptableObject>(
            (asset, reference) => 
            {
                _componentIcon.sprite = _spriteFactory.GetAsset(asset);
                _componentText.text = asset.name;
            },
            null
        );
        
        _takeJunkButton.onClick.AddListener(() =>
        {
            _viewModel.ShouldTakeComponent = true;
            Close();
        });
    }
}