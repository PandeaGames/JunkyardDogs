using UnityEngine;


public class JunkyardTesting : MonoBehaviour
{
    [SerializeField]
    private JunkyardMonoView _junkyardMonoView;
    public JunkyardMonoView JunkyardMonoView
    {
        get { return _junkyardMonoView; }
    }
    
    [SerializeField]
    private JunkyardConfig _junkyardConfig;
    public JunkyardConfig JunkyardConfig
    {
        get { return _junkyardConfig; }
    }
    
    [SerializeField]
    private JunkyardData _junkyardData;
    public JunkyardData JunkyardData
    {
        get { return _junkyardData; }
    }
}
