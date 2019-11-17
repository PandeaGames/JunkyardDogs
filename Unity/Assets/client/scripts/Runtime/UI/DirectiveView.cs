using System;
using JunkyardDogs.Simulation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Directive = JunkyardDogs.Components.Directive;

public enum ShowHideSetting
{
    HideWhenShowingNull,
    ShowWhenShowingNull
}
    
[Serializable]
public struct DisplaySetting
{
    public GameObject obj;
    public ShowHideSetting showSetting;
}

[RequireComponent(typeof(Button))]
public class DirectiveView : MonoBehaviour
{
    public event Action<DirectiveView> OnClick;

    [SerializeField]
    private TMP_Text _directiveName;

    private Button _button;

    [SerializeField] private DisplaySetting[] displaySettings;

    public void SetupSpecification(JunkyardDogs.Specifications.Directive directive)
    {
        _button = GetComponent<Button>();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(Click);
        
        if (directive != null)
        {
            _directiveName.text = directive.name;
        }
        else
        {
            _directiveName.text = "---";
        }

        ApplyDisplaySettings(directive != null);
    }
    
    public void SetupComponent(Directive directive)
    {
        if (directive != null)
        {
            SetupSpecification(directive.GetSpec());
        }
        else
        {
            SetupSpecification(null);
        }
    }

    private void Click()
    {
        OnClick(this);
    }

    private void ApplyDisplaySettings(bool hasDirective)
    {
        foreach (DisplaySetting setting in displaySettings)
        {
            bool active = false;
            switch (setting.showSetting)
            {
                case ShowHideSetting.HideWhenShowingNull:
                    active = hasDirective;
                    break;
                case ShowHideSetting.ShowWhenShowingNull:
                    active = !hasDirective;
                    break;
            }
            
            setting.obj.SetActive(active);
        }
    }
}
