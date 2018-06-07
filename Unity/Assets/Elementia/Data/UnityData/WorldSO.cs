using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Data;
using WeakReference = Data.WeakReference;

public class WorldSO : ScriptableObject
{
    [SerializeField]
    [WeakReference(typeof(LayerSO))]
    private List<WeakReference> _layers;
    public List<WeakReference> layers { get { return _layers; } }

    [MenuItem("Elementia Data/World/World")]
    static void DoIt()
    {
        WorldSO asset = ScriptableObject.CreateInstance<WorldSO>();
        AssetDatabase.CreateAsset(asset, "Assets/WorldSO.asset");
        AssetDatabase.SaveAssets();
    }

    public static Area GenerateArea(AreaSO areaSO, WorldSO world)
    {
        Area area = new Area(areaSO.dimensions);
        area.Layers = new List<Layer>();

        foreach (WeakReference layerSORefernece in world.layers)
        {
            LayerSO layerSO = layerSORefernece.Load<LayerSO>();
            Layer layer = new Layer(areaSO.dimensions, layerSORefernece);
            area.Layers.Add(layer);
        }

        return area;
    }
}