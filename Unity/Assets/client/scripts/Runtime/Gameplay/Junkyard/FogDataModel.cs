﻿using System;
using UnityEngine;

public class FogDataPoint : GridDataPoint<int>
{
}

public class FogDataModel : AbstractGridDataModel<int, FogDataPoint>
{
    public FogDataModel(Junkyard junkyard, JunkyardConfig config) : base(InitializeData(junkyard, config))
    {

    }

    private static int[,] InitializeData(Junkyard junkyard, JunkyardConfig config)
    {
        int[,] data = new int[junkyard.Width,junkyard.Height];
        
        foreach (INTVector vector in junkyard.GetGridSpaces())
        {
            int value = GetValue(junkyard, config, vector);
            data[vector.X, vector.Y] = value;
        }
        
        return data;
    }
    
    private static int GetValue(Junkyard junkyard, JunkyardConfig config, INTVector vector)
    {
        return GetValue(junkyard, config, vector.X, vector.Y);
    }

    private static int GetValue(Junkyard junkyard, JunkyardConfig config, int xIN, int yIN)
    {
        int totalFogDepth = config.FogDepth;
        int closestClearedData = int.MaxValue;

        for (int x = Math.Max(0, xIN - totalFogDepth); x < Math.Min(junkyard.Width, xIN + totalFogDepth); x++)
        {
            for (int y = Math.Max(0, yIN - totalFogDepth); y < Math.Min(junkyard.Height, yIN + totalFogDepth); y++)
            {
                if (junkyard.GetCleared(x, y))
                {
                    closestClearedData = Math.Min(closestClearedData, Math.Max(Math.Abs(x - xIN), Math.Abs(y - yIN)));
                }
            }
        }

        return config.GetIndexAtFogDepth(closestClearedData);
    }
    
    private byte GetValue(INTVector vector)
    {
        return 0;
    }

    private byte GetValue(uint x, uint y)
    {
        return 0;
    }
    
}
