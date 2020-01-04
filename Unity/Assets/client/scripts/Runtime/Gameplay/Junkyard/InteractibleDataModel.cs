using System;
using UnityEngine;

public class InteractibleGridDataPoint : GridDataPoint<bool>
{
}

public class InteractibleDataModel : AbstractGridDataModel<bool, InteractibleGridDataPoint>
{
    public InteractibleDataModel(FogDataModel fogDataModel, JunkyardConfig junkyardConfig) : base(InitializeData(fogDataModel, junkyardConfig))
    {

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
}
