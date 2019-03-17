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
        _componentIcon.sprite = _spriteFactory.GetAsset( _viewModel.ModelData.Component.SpecificationReference.Data);
        _componentText.text = _viewModel.ModelData.Component.SpecificationReference.Data.name;
        
        _takeJunkButton.onClick.AddListener(() =>
        {
            _viewModel.ShouldTakeComponent = true;
            Close();
        });
    }
}