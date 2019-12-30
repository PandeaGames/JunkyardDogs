using UnityEngine;
using UnityEngine.UI;

public class JunkyardMiniMap : MonoBehaviour
{
    [SerializeField] private RawImage _uiImage;
    [SerializeField] private Camera _renderCamera;
    
    public void Setup(Junkyard junkyard)
    {
        int size = junkyard.Width / 2;

        _renderCamera.orthographicSize = size;
        _renderCamera.transform.position = new Vector3(size, size, size);
        _renderCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
