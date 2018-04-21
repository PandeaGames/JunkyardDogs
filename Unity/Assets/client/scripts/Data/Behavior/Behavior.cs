using UnityEngine;
using System.Collections.Generic;

namespace JunkyardDogs.Behavior
{
    public class Behavior : ScriptableObject
    {
        [SerializeField]
        private List<Action> _actions;
    }
}