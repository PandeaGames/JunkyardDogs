using UnityEngine;

public class SimpleAgent : CameraAgent
{
    public override Vector3 GetCameraPosition()
    {
        return transform.position;
    }

    public override Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }
}