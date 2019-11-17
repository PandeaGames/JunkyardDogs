using UnityEngine;
using JunkyardDogs.Components;
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
    }
}
