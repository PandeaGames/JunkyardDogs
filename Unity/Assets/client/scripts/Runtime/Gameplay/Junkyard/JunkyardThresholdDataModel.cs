using System;
using System.Collections.Generic;
using UnityEngine;

public class JunkyardThresholdDataPoint : GridDataPoint<int>
{
}

public class JunkyardThresholdDataModel : AbstractGridDataModel<int, JunkyardThresholdDataPoint>
{
    private JunkyardConfig _junkyardConfig;
    
    public JunkyardThresholdDataModel(Junkyard junkyard, JunkyardConfig junkyardConfig) : base(InitializeData(junkyard, junkyardConfig))
    {
        _junkyardConfig = junkyardConfig;
    }

    private static int[,] InitializeData(Junkyard junkyard, JunkyardConfig junkyardConfig)
    {
        int[,] data = new int[junkyard.Width,junkyard.Height];

        for (int x = 0; x < junkyard.Width; x++)
        {
            for (int y = 0; y < junkyard.Width; y++)
            {
                for (int i = 0; i < junkyardConfig.Layers.Length; i++)
                {
                    JunkyardConfig.JunkyardLayerConfig layerConfig = junkyardConfig.Layers[i];

                    if (layerConfig.threshold > junkyard.serializedJunkyard.Data[x, y])
                    {
                        data[x, y] = i;
                        break;
                    }
                }
            }
        }
        
        return data;
    }
}
