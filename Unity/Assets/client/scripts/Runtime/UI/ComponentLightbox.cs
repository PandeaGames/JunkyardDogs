using System;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;

public class ComponentLightbox : MonoBehaviour
{    
    private LightboxCamera _lightboxCameraCache;
    private LightboxCamera _lightboxCamera
    {
        get
        {
            if (_lightboxCameraCache == null)
            {
                GameObject cameraGameObject = new GameObject();
                cameraGameObject.transform.parent = transform;
                cameraGameObject.transform.position = Vector3.back * 3;
                _lightboxCameraCache = cameraGameObject.AddComponent<LightboxCamera>();
            }

            return _lightboxCameraCache;
        }
    }
    
    private PrefabFactory _prefabFactoryCache;
    private PrefabFactory _prefabFactory
    {
        get
        {
            if(_prefabFactoryCache == null)
            {
                _prefabFactoryCache = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData
                    .ComponentPrefabFactory;
            }

            return _prefabFactoryCache;
        }
    }
    
    private GameObject _componentObject;
    
    public void GetComponentTexture(IComponent component, Action<Texture2D> onComplete, Action onError)
    {
        Texture2D texture = null;

        _componentObject = _prefabFactory.InstantiateAsset(component.Specification, transform, false);
        _lightboxCamera.GetTexture(tex =>
        {
            Destroy(_componentObject);
            onComplete(tex);
        }, onError);
    }
}
