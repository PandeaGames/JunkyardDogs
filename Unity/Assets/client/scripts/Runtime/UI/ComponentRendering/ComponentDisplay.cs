using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Component = JunkyardDogs.Components.Component;
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
        Component component = data as Component;

        if(component != null)
        {
            RenderComponent(component);
        }
    }

    // Use this for initialization
    public void RenderComponent(Component component)
    {
        _componentIcon.sprite = _spriteFactory.GetAsset(component.SpecificationReference.Data);
        _componentName.text = component.SpecificationReference.Data.name;
    }
}
