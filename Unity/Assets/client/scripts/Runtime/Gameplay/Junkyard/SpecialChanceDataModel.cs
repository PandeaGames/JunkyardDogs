using System;
using System.Collections.Generic;
using UnityEngine;

public class SpecialChanceDataPoint : GridDataPoint<bool>
{
}

public class SpecialChanceDataModel : AbstractGridDataModel<bool, SpecialChanceDataPoint>
{
    private Junkyard _junkyard;
    
    public SpecialChanceDataModel(Junkyard junkyard) : base(InitializeData(junkyard))
    {
        _junkyard = junkyard;
    }

    private static bool[,] InitializeData(Junkyard junkyard)
    {
        bool[,] data = new bool[junkyard.Width,junkyard.Height];

        int i = 0;
        
        foreach (INTVector dataPoint in junkyard.GetGridSpaces())
        {
            UnityEngine.Random.seed = junkyard.serializedJunkyard.Seed + i++;
            float randomValue = UnityEngine.Random.Range(0, 100);
            bool value = randomValue < junkyard.ChanceForSpecial;
            data[dataPoint.X, dataPoint.Y] = value;
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
