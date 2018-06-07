using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using Data;
using WeakReference = Data.WeakReference;

[Serializable]
public struct AreaDimensions
{
    [SerializeField]
    public int x;

    [SerializeField]
    public int y;

    public int Area { get { return x * y; } }
}

public class AreaSO : ScriptableObject
{
    [SerializeField]
    private AreaDimensions _dimensions;
    public AreaDimensions dimensions { get { return _dimensions; } }

    [MenuItem("Elementia Data/Blueprints/Area")]
    static void DoIt()
    {
        AreaSO asset = ScriptableObject.CreateInstance<AreaSO>();
        AssetDatabase.CreateAsset(asset, "Assets/AreaBlueprint.asset");
        AssetDatabase.SaveAssets();
    }

    public static void StepArea(Area area, AreaSO areaSO, AreaSteper areaSteper)
    {
        Dictionary<Layer, LayerSO> layerTable = new Dictionary<Layer, LayerSO>();

        foreach (Layer layer in area.Layers)
        {
            layerTable.Add(layer, layer.LayerReference.Load<LayerSO>());
        }

        areaSteper.Step(layerTable, areaSO.dimensions);
    }
}

public class AreaSteper
{
    private Dictionary<Layer, LayerSO> _layerTable;
    private bool[,] _updateTable;
    private AreaDimensions _dimensions;

    public void Step(Dictionary<Layer, LayerSO> layerTable, AreaDimensions dimensions)
    {
        _layerTable = layerTable;
        _updateTable = new bool[dimensions.x, dimensions.y];
        _dimensions = dimensions;
        DoStep(0, 0);
    }

    protected void DoStep(int x, int y)
    {
        StepPoint(x, y);

        int left = x - 1;
        int right = x + 1;
        int top = y - 1;
        int bottom = y + 1;

        if (x == 0)
        {
            left = 0;
        }

        if (x == _dimensions.x - 1)
        {
            right = 0;
        }

        if (y == 0)
        {
            top = 0;
        }

        if (y == _dimensions.y - 1)
        {
            top = 0;
        }

        for (int i = x-left;i<x+right;i++)
        {
            for (int j = y - top; j < y + bottom; j++)
            {
                if (!_updateTable[i, j])
                {
                    DoStep(i, j);
                }
            }
        }
    }

    protected bool StepPoint(int x, int y)
    {
        _updateTable[x, y] = true;

        foreach (KeyValuePair<Layer, LayerSO> keyValuePair in _layerTable)
        {
            LayerSO layerSO = keyValuePair.Value;
            layerSO.StepLayerPoint(_layerTable);
        }

        return false;
    }
}