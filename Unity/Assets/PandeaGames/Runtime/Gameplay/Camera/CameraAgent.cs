using System.Collections;
using System.Collections.Generic;
using PandeaGames;
using UnityEngine;

public abstract class CameraAgent : MonoBehaviour {

    [SerializeField]
    private float scale = 1;

    protected CameraViewModel CameraViewModel;

    public abstract Vector3 GetCameraPosition();

    public void Start()
    {
        CameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
    }

    public virtual Quaternion GetCameraRotation()
    {
        return new Quaternion();
    }

    public virtual void FocusStart(CameraMaster master)
    {

    }

    public virtual void FocusEnd(CameraMaster master)
    {

    }

    public float GetCameraOrthographicScale()
    {
        return scale;
    }
}