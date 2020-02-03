using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JunkyardDogs.Simulation.Agent;
using JunkyardDogs.Simulation.Behavior;
using UnityEngine.UI;

public class DirectiveDisplay : MonoBehaviour {
    [SerializeField]
    private TMP_Text _text;
    
    [SerializeField]
    private Image _image;

    private ActionDirective _actionDirective;
    public ActionDirective ActionDirective
    {
        get { return _actionDirective; }
    }
    
    public void Render(ActionDirective actionDirective)
    {
        _actionDirective = actionDirective;
        _text.text = _actionDirective.BehaviorAction.name;
    }
}