using System;
using System.Collections;
using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;

public class LightboxCamera : MonoBehaviour
{
    private RenderTexture _renderTextureCache;
    private RenderTexture _renderTexture
    {
        get
        {
            if(_renderTextureCache == null)
            {
                _renderTextureCache = new RenderTexture(200,200,0);
            }

            return _renderTextureCache;
        }
    }
    
    private Camera _cameraCache;
    private Camera _camera
    {
        get
        {
            if (_cameraCache == null)
            {
                GameObject cameraGameObject = new GameObject();
                cameraGameObject.transform.parent = transform;
                cameraGameObject.transform.position = Vector3.back * 3;
                _cameraCache = gameObject.AddComponent<Camera>();
                _cameraCache.backgroundColor = Color.green;
                _cameraCache.clearFlags = CameraClearFlags.SolidColor;
                _cameraCache.targetTexture = _renderTexture;
            }

            return _cameraCache;
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
                    .BotPrefabFactory;
            }

            return _prefabFactoryCache;
        }
    }
    
    public void GetTexture(Action<Texture2D> onComplete, Action onError)
    {
        StartCoroutine(GetTextureCoroutine(onComplete, onError));
    }

    private Texture2D _textureWaitingToBeDrawn;
    private bool isTextureDrawn;
    private IEnumerator GetTextureCoroutine(Action<Texture2D> onComplete, Action onError)
    {
        yield return null;

        Texture2D texture = null;

        if (texture == null)
        {
            _camera.enabled = true;
            texture = new Texture2D(200, 200);
            _textureWaitingToBeDrawn = texture;
            isTextureDrawn = false;
            RenderTexture.active = _renderTexture;
            
            while (!isTextureDrawn)
            {
                yield return null;
            }
        }

        isTextureDrawn = false;
        onComplete(texture);
    }

    private void OnPostRender()
    {
        if (_textureWaitingToBeDrawn != null)
        {
            _textureWaitingToBeDrawn.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
            _textureWaitingToBeDrawn.Apply();
            _textureWaitingToBeDrawn = null;
            isTextureDrawn = true;
        }
    }
}
