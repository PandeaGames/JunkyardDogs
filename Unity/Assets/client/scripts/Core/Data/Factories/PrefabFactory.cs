using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class PrefabFactory : ObjectFactory<GameObject, PrefabManifestEntry>
{
    public GameObject InstantiateAsset(ScriptableObject obj)
    {
        GameObject prefab = GetAsset(obj);
        return GameObject.Instantiate(prefab);
    }
}

[Serializable]
public class PrefabManifestEntry : ObjectManifestEntry<GameObject>
{
}