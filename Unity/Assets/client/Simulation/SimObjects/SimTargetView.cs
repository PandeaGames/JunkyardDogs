using System;
using JunkyardDogs.Simulation;
using UnityEngine;

public class SimTargetView : MonoBehaviour
{
    private SimPhysicsObject _simPhysicsObject;
    public void Follow(SimPhysicsObject simPhysicsObject)
    {
        _simPhysicsObject = simPhysicsObject;
    }

    private void LateUpdate()
    {
        if (_simPhysicsObject != null)
        {
            transform.position = new Vector3(
                _simPhysicsObject.body.position.x, 
                0, 
                _simPhysicsObject.body.position.y
                );
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 
                _simPhysicsObject.body.rotation.deg360 * -1, 0));
        }
    }
}
