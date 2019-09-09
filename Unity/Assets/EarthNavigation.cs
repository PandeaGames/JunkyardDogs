using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthNavigation : MonoBehaviour
{
    [SerializeField]
    private CameraAgent _cameraAgent;
    public CameraAgent cameraAgent
    {
        get { return _cameraAgent; }
    }
}
