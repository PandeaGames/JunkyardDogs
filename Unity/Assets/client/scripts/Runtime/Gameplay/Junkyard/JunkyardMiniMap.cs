using JunkyardDogs;
using PandeaGames;
using UnityEngine;
using UnityEngine.UI;

public class JunkyardMiniMap : MonoBehaviour
{
    [SerializeField] private RawImage _uiImage;
    [SerializeField] private Camera _renderCamera;

    private JunkyardViewModel _junkyardViewModel;
    private Camera _camera;

    private void Start()
    {
        _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
        int size = _junkyardViewModel.junkyard.Width / 2;
        
        GameObject camGO = new GameObject("MiniMapCamera");

        _camera = camGO.AddComponent<Camera>();
        _camera.targetTexture = _uiImage.mainTexture as RenderTexture;

        _camera.orthographic = true;
        _camera.orthographicSize = size;
        _camera.transform.position = new Vector3(size, size, size);
        _camera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    private void OnDestroy()
    {
        Destroy(_camera.gameObject);
    }

    private void OnEnable()
    {
        if(_camera != null) _camera.gameObject.SetActive(true);
    }
    
    private void OnDisable()
    {
        if(_camera != null) _camera.gameObject.SetActive(false);
    }
}
