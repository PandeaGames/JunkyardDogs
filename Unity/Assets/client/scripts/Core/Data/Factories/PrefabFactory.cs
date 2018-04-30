using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class PrefabFactory : ObjectFactory<GameObject, PrefabManifestEntry>
{
}

[Serializable]
public class PrefabManifestEntry : ObjectManifestEntry<GameObject>
{
}