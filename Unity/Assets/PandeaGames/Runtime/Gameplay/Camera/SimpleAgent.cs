using UnityEngine;

public class SimpleAgent : CameraAgent
{
    [SerializeField] private bool _enforceFixedRotation;
    [SerializeField] private Vector3 _fixedRotation;
    [SerializeField] private bool _enforceFixedPosition;
    [SerializeField] private Vector3 _fixedPosition;
    
    public override Vector3 GetCameraPosition()
    {
        return _enforceFixedPosition ? _fixedPosition:transform.position;
    }

    public override Quaternion GetCameraRotation()
    {
        return _enforceFixedRotation ? Quaternion.Euler(_fixedRotation): transform.rotation;
    }
}