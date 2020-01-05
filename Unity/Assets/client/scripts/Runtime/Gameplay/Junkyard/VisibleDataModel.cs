using System;
using System.Collections.Generic;
using System.Linq;
using SRF.UI;
using UnityEngine;

public class VisibleGridDataPoint : GridDataPoint<bool>
{
}

public class VisibleDataModel : AbstractGridDataModel<bool, VisibleGridDataPoint>
{
    private FogDataModel _fogDataModel;
    private JunkyardConfig _junkyardConfig;
    
    public VisibleDataModel(FogDataModel fogDataModel, JunkyardConfig junkyardConfig) : base(InitializeData(fogDataModel, junkyardConfig))
    {
        _fogDataModel = fogDataModel;
        _junkyardConfig = junkyardConfig;
        fogDataModel.OnDataHasChanged += OnFogDataHasChanged;
    }

    private void OnFogDataHasChanged(IEnumerable<FogDataPoint> fogData)
    {
        foreach (FogDataPoint dataPoint in fogData)
        {
            this[dataPoint.Vector] = _junkyardConfig.GetLayerDataAtIndex(dataPoint.Data).visible;
        }
        
        DataHasChanged(DataChangeFromFog(fogData));
    }

    private IEnumerable<VisibleGridDataPoint> DataChangeFromFog(IEnumerable<FogDataPoint> fogData)
    {
        foreach (FogDataPoint dataPoint in fogData)
        {
            yield return new VisibleGridDataPoint {Vector = dataPoint.Vector, Data = this[dataPoint.Vector]};
        }
    }
    
    private static bool[,] InitializeData(FogDataModel fogDataModel, JunkyardConfig junkyardConfig)
    {
        bool[,] data = new bool[fogDataModel.Width,fogDataModel.Height];
        
        foreach (FogDataPoint dataPoint in fogDataModel.AllData())
        {
            int value = dataPoint.Data;
            data[dataPoint.Vector.X, dataPoint.Vector.Y] = junkyardConfig.GetLayerDataAtIndex(value).visible;
        }
        
        return data;
    }
}
