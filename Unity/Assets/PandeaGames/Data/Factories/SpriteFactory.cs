using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class SpriteFactory : ObjectFactory<Sprite, SpriteManifestEntry>
{
}

[Serializable]
public class SpriteManifestEntry : ObjectManifestEntry<Sprite>
{
}