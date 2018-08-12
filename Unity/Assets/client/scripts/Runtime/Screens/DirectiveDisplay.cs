using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JunkyardDogs.Simulation.Agent;
using JunkyardDogs.Simulation.Behavior;

public class DirectiveDisplay : MonoBehaviour {
    [SerializeField]
    private TMP_Text _text;

    private Directive _directive;
    public Directive Directive
    {
        get { return _directive; }
    }
    
    public void Render(Directive directive)
    {
        _directive = directive;
        directive.ActionWeakReference.LoadAsync<Action>((action, reference) =>
        {
            _text.text = action.name;
        }, () => { });
    }
}