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
    [SerializeField] private Material _fogOfWar;
    [SerializeField] private GameObject _wall;
    [SerializeField] private BotRenderConfiguration _botRenderConfiguration;
    [SerializeField] private float _wallWidth;
    [SerializeField] private GameObject _junkClearedAnimation;

    
    
    public BotRenderConfiguration BotRenderConfiguration
    {
        get { return _botRenderConfiguration; }
    }
    
    public Material GroundMaterial
    {
        get { return _groundMaterial; }
    }
    
    public Material FogOfWar
    {
        get { return _fogOfWar; }
    }
    
    public GameObject Wall
    {
        get { return _wall; }
    }
    
    public float WallWidth
    {
        get { return _wallWidth; }
    }
    
    public GameObject JunkClearedAnimation
    {
        get { return _junkClearedAnimation; }
    }
}
