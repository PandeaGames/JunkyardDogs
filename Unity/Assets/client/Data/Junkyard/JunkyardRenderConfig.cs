using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardRenderConfig")]
public class JunkyardRenderConfig : ScriptableObject
{
    [Serializable]
    public class JunkyardLayerRenderConfig
    {
        public GameObject prefab;
        public GameObject clearedPrefab;
    }

    public JunkyardLayerRenderConfig[] configs;

    public JunkyardLayerRenderConfig[] Configs
    {
        get { return configs; }
    }

    [SerializeField] private Material _groundMaterial;

    public Material GroundMaterial
    {
        get { return _groundMaterial; }
    }
}
