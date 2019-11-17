using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using PandeaGames;
using UnityEngine;

public class ComponentTextureProvider : MonoBehaviourSingleton<ComponentTextureProvider>
{
    private Dictionary<IComponent, Texture2D> _cache = new Dictionary<IComponent, Texture2D>();

    private ComponentLightbox _componentLightboxCache;
    private ComponentLightbox _componentLightbox
    {
        get
        {
            if (_componentLightboxCache == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.parent = transform;
                _componentLightboxCache = gameObject.AddComponent<ComponentLightbox>();
            }

            return _componentLightboxCache;
        }
    }

    public void GetComponentTexture(IComponent component, Action<Texture2D> onComplete, Action onError)
    {
        StartCoroutine(GetComponentTextureCoroutine(component, onComplete, onError));
    }

    private IEnumerator GetComponentTextureCoroutine(IComponent component, Action<Texture2D> onComplete, Action onError)
    {
        yield return null;

        Texture2D texture = null;
        _cache.TryGetValue(component, out texture);

        if (texture == null)
        {
            _componentLightbox.GetComponentTexture(component, tex =>
            {
                _cache.Add(component, tex);
                onComplete(tex);
            }, onError);
        }
        else
        {
            onComplete(texture);
        }
    }
}
