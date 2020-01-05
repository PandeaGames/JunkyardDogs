using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGridDataPoint : GridDataPoint<bool>
{
}

public class InteractibleDataModel : AbstractGridDataModel<bool, InteractibleGridDataPoint>
{
    private JunkyardConfig _junkyardConfig;
    
    public InteractibleDataModel(FogDataModel fogDataModel, JunkyardConfig junkyardConfig) : base(InitializeData(fogDataModel, junkyardConfig))
    {
        _junkyardConfig = junkyardConfig;
        fogDataModel.OnDataHasChanged += OnFogDataHasChanged;
    }
    
    private void OnFogDataHasChanged(IEnumerable<FogDataPoint> fogData)
    {
        foreach (FogDataPoint dataPoint in fogData)
        {
            this[dataPoint.Vector] = _junkyardConfig.GetLayerDataAtIndex(dataPoint.Data).interactible;
        }
        
        DataHasChanged(DataChangeFromFog(fogData));
    }

    private static bool[,] InitializeData(FogDataModel fogDataModel, JunkyardConfig junkyardConfig)
    {
        bool[,] data = new bool[fogDataModel.Width,fogDataModel.Height];
        
        foreach (FogDataPoint dataPoint in fogDataModel.AllData())
        {
            int value = dataPoint.Data;
            data[dataPoint.Vector.X, dataPoint.Vector.Y] = junkyardConfig.GetLayerDataAtIndex(value).interactible;
        }
        
        return data;
    }
    
    private IEnumerable<InteractibleGridDataPoint> DataChangeFromFog(IEnumerable<FogDataPoint> fogData)
    {
        foreach (FogDataPoint dataPoint in fogData)
        {
            yield return new InteractibleGridDataPoint {Vector = dataPoint.Vector, Data = this[dataPoint.Vector]};
        }
    }
}
