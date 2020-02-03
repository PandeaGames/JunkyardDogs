using JunkyardDogs.Simulation;
using UnityEngine;


public class BotFollowCameraMonoView : CameraAgent
{
    private SimBot _target;
    private SimBot _other;

    private Vector3 _trackingPosition;
    
    public void Setup(SimBot target, SimBot otherTarget)
    {
        _target = target;
        _other = otherTarget;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 botPosition = _target.body.PositionToWorld();
        Vector3 otherBotPosition = _other.body.PositionToWorld();
        Vector3 diff = otherBotPosition - botPosition;
        Vector3 normalizedDiff = diff / 3 * -1;
        Vector3 position = botPosition + normalizedDiff;
        position = new Vector3(position.x, normalizedDiff.magnitude + 3, position.z);

        if (_trackingPosition == Vector3.zero)
        {
            _trackingPosition = position;
        }
        else
        {
            _trackingPosition = _trackingPosition + ((position - _trackingPosition) / 20);
        }
        
        transform.position = _trackingPosition;
        transform.LookAt(otherBotPosition);
    }

    public override Vector3 GetCameraPosition()
    {
        return transform.position;
    }

    public override Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }
}
