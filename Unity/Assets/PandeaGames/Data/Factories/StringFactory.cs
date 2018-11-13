using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class StringFactory : ObjectFactory<string, StringManifestEntry>
{
}

[Serializable]
public class StringManifestEntry : ObjectManifestEntry<string>
{
}