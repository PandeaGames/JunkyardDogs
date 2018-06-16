using UnityEngine;
using System.Collections;

public class LineupCameraAgent : SimpleFollowAgent
{
    public override void Update()
    {
        base.Update();
        transform.LookAt(_target);
    }

    public override Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }
}
