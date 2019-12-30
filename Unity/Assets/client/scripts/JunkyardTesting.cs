using UnityEngine;


public class JunkyardTesting : MonoBehaviour
{
    [SerializeField]
    private JunkyardMonoView _junkyardMonoView;
    public JunkyardMonoView JunkyardMonoView
    {
        get { return _junkyardMonoView; }
    }
}
