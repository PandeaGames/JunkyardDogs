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
        ILoot loot = _viewModel.ModelData.Loot[0];
        Component component = LootUtilities.TryCreateComponentFromLoot(loot, 0);
        
        _componentIcon.sprite = _spriteFactory.GetAsset( component.SpecificationReference.Data);
        _componentText.text = component.SpecificationReference.Data.name;
        
        _takeJunkButton.onClick.AddListener(() =>
        {
            _viewModel.ShouldTakeLoot = true;
            Close();
        });
    }
}