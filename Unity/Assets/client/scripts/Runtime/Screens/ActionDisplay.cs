using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JunkyardDogs.Simulation.Behavior;

public class ActionDisplay : MonoBehaviour {
    [SerializeField]
    private TMP_Text _text;

    public void Render(Action directive)
    {
        _text.text = directive.name;
    }
}