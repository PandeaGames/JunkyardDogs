using UnityEngine;

public class LinkUITo : MonoBehaviour
{
    [SerializeField] 
    private Transform _3DTransformLink;
    
    private void Update()
    {
        if (_3DTransformLink != null)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(_3DTransformLink.position);
            transform.position = position;
        }
    }
}
