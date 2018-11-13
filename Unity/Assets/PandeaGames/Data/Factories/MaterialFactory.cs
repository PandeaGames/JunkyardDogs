using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class MaterialFactory : ObjectFactory<Material, MaterialManifestEntry>
{
}

[Serializable]
public class MaterialManifestEntry : ObjectManifestEntry<Material>
{
}
