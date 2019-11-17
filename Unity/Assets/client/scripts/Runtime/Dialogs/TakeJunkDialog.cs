﻿using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.scripts.Runtime.Dialogs;
using UnityEngine.UI;
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
        IComponent component = LootUtilities.TryCreateComponentFromLoot(loot, 0);
        
        _componentIcon.sprite = _spriteFactory.GetAsset( component.SpecificationReference.Data);
        _componentText.text = component.SpecificationReference.Data.name;
        
        _takeJunkButton.onClick.AddListener(() =>
        {
            _viewModel.ShouldTakeLoot = true;
            Close();
        });

        ComponentTextureProvider.Instance.GetComponentTexture(component, OnTextureFound, OnError);
    }

    private void OnTextureFound(Texture2D texture)
    {
        _componentIcon.material.SetTexture("Video (RGB)", texture);
        _componentIcon.material.SetTexture("Base (RGB)", texture);
        _componentIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), _componentIcon.sprite.pixelsPerUnit);
    }

    private void OnError()
    {
        
    }
}