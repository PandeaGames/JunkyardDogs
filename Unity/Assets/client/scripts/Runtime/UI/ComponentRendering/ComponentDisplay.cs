using UnityEngine;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using PandeaGames;
using TMPro;
using UnityEngine.UI;
using SRF.UI.Layout;

public class ComponentDisplay : MonoBehaviour, IVirtualView
{
    [SerializeField]
    private TMP_Text _componentName;

    [SerializeField]
    private SpriteFactory _spriteFactory;

    [SerializeField]
    private Image _componentIcon;

    [SerializeField] 
    private Image[] _rarityImages;
    
    [SerializeField] 
    private TMP_Text[] _rarityHighlightText;
    
    [SerializeField]
    private TMP_Text _gradeTxt;

    public void SetDataContext(object data)
    {
        IComponent component = data as IComponent;

        if(component != null)
        {
            RenderComponent(component);
        }
    }

    // Use this for initialization
    public void RenderComponent(IComponent component)
    {
        _componentIcon.sprite = _spriteFactory.GetAsset(component.SpecificationReference.Data);
        _componentName.text = component.SpecificationReference.Data.name;

        RarityArtConfig rarityConfig =
            GameStaticDataProvider.Instance.GetRarityArtConfig((int)component.Specification.Rarity.Value);
        
        foreach (Image rarityImage in _rarityImages)
        {
            rarityImage.color = rarityConfig.Color;
        }
        
        foreach (TMP_Text rarityHighlughtTxt in _rarityHighlightText)
        {
            rarityHighlughtTxt.color = rarityConfig.HighlightColor;
        }

        if (_gradeTxt != null)
        {
            _gradeTxt.text = component.Specification.Grade.ToString();
        }
    }
}
