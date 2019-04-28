using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;
using Component = JunkyardDogs.Components.Component;

public class ComponentTextureProvider : MonoBehaviourSingleton<ComponentTextureProvider>
{
    private Dictionary<Component, Texture2D> _cache = new Dictionary<Component, Texture2D>();

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

    public void GetComponentTexture(Component component, Action<Texture2D> onComplete, Action onError)
    {
        StartCoroutine(GetComponentTextureCoroutine(component, onComplete, onError));
    }

    private IEnumerator GetComponentTextureCoroutine(Component component, Action<Texture2D> onComplete, Action onError)
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
