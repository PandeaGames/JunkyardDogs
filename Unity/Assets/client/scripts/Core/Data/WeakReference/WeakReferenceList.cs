using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Data {
    public class WeakReferenceList<T> : ScriptableObject
    {
        [SerializeField]
        private List<WeakReference> _list;
    }
}
