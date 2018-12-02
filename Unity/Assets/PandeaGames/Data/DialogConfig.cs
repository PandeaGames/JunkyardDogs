using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DialogConfig", menuName = "PandeaGaames/Config/DialogConfig", order = 1)]
public class DialogConfig : ScriptableObject, IEnumerable
{
    [SerializeField]
    private GameObject[] _dialogs;

    public IEnumerator GetEnumerator()
    {
        return _dialogs.GetEnumerator();
    }
}