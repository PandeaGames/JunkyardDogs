using UnityEngine;
using System.Collections;
using Data;
using WeakReference = Data.WeakReference;

public class Layer
{
    public byte[,] Values { get; set; }
    public WeakReference LayerReference { get; set; }

    public Layer()
    {

    }

    public Layer(AreaDimensions dimensions, WeakReference layerSORefernece)
    {
        Values = new byte[dimensions.x, dimensions.y];
        LayerReference = layerSORefernece;
    }
}
