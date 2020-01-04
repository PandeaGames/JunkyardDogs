using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardConfig")]
public class JunkyardConfig : ScriptableObject
{
    [Serializable]
    public class JunkyardFogLayerConfig
    {
        public int depth;
        public bool interactible;
    }

    [Serializable]
    public class JunkyardLayerConfig
    {
        public int cost;
        public byte threshold;
    }

    public JunkyardLayerConfig[] _layers;

    public JunkyardLayerConfig[] Layers
    {
        get { return _layers; }
    }

    public JunkyardFogLayerConfig[] _fogLayers;

    public JunkyardFogLayerConfig[] FogLayers
    {
        get { return _fogLayers; }
    }

    public int FogDepth
    {
        get
        {
            int fogDepth = 0;
            foreach (JunkyardFogLayerConfig fogLayerConfig in _fogLayers)
            {
                fogDepth += fogLayerConfig.depth;
            }

            return fogDepth;
        }
    }

    public int GetIndexAtFogDepth(int fogDepth)
    {
        int depth = 0;
        for (int i = 0; i < _fogLayers.Length; i++)
        {
            JunkyardFogLayerConfig fogLayerConfig = _fogLayers[i];
            depth += fogLayerConfig.depth;
            if (depth >= fogDepth)
            {
                return i;
            }
        }

        return 0;
    }
}