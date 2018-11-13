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
        component.SpecificationReference.LoadAssetAsync<ScriptableObject>(
            (asset, reference) =>
            {
                _componentIcon.sprite = _spriteFactory.GetAsset(asset);
                _componentName.text = asset.name;
            },
            null
            );
    }
}
