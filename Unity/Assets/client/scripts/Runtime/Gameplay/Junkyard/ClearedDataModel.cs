using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ClearedDataPoint : GridDataPoint<bool>
{
}

public class ClearedDataModel : AbstractGridDataModel<bool, ClearedDataPoint>
{
    private Junkyard _junkyard;
    
    public ClearedDataModel(Junkyard junkyard) : base(InitializeData(junkyard))
    {
        _junkyard = junkyard;
    }

    private static bool[,] InitializeData(Junkyard junkyard)
    {
        bool[,] data = new bool[junkyard.Width,junkyard.Height];

        int i = 0;
        
        foreach (INTVector dataPoint in junkyard.GetGridSpaces())
        {
            data[dataPoint.X, dataPoint.Y] = junkyard.serializedJunkyard.Cleared[dataPoint.X, dataPoint.Y];
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
