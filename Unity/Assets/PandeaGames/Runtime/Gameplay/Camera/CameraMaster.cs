using System.Collections;
using System.Collections.Generic;
using PandeaGames;
using UnityEngine;

public class CameraMaster : MonoBehaviour {

    [SerializeField]
    private Camera _camera;
     

    public Camera Camera => _camera;

    [SerializeField]
    private CameraAgent _cameraAgent;

    private CameraViewModel _cameraViewModel;

    public void Focus(CameraAgent agent = null)
    {
        _cameraAgent = agent;
    }

    // Use this for initialization
    void Start ()
    {
        _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
        _cameraViewModel.RegisterMaster(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_cameraAgent != null)
            UpdateCameraPosition(_cameraAgent);
	}

    private void UpdateCameraPosition(CameraAgent agent)
    {
        Vector3 pos = agent.GetCameraPosition();

        transform.position = new Vector3(pos.x, pos.y, pos.z);
        transform.rotation = agent.GetCameraRotation();
        
        _camera.orthographicSize = agent.GetCameraOrthographicScale();
    }
}
