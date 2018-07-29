using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class ScriptableObjectFactory : ObjectFactory<ScriptableObject, ScriptableObjectManifestEntry>
{
}

[Serializable]
public class ScriptableObjectManifestEntry : ObjectManifestEntry<ScriptableObject>
{
}