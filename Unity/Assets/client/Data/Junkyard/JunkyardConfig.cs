using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardConfig")]
public class JunkyardConfig : ScriptableObject
{
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
}
