using UnityEngine;

public class SimpleFollowAgent : CameraAgent
{
    [SerializeField]
    protected Transform _target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField][Range(float.Epsilon, 1)]
    private float _smoothing = 1;

    [SerializeField] private bool _shouldLookAtTarget;

    public Transform Target { get { return _target; } }

    public void SetTarget(Transform transform)
    {
        _target = transform;
    }

    public virtual void Update()
    {
        if (_target == null)
            return;
        
        Vector3 delta = (_target.position + offset) - transform.position;
        Vector3 position = transform.position + delta * _smoothing;
        transform.position = new Vector3(position.x, position.y, position.z);

        if (_shouldLookAtTarget)
        {
            transform.LookAt(_target);
        }
    }

    public override Vector3 GetCameraPosition()
    {
        if (_target == null)
            return Vector3.zero;

        return transform.position;
    }

    public override Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }
}