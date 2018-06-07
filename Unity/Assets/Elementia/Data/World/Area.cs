using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Area
{
    public int x { get; set; }
    public int y { get; set; }
    public List<Layer> Layers { get; set; }

    public int GetArea { get { return x * y; } }

    public Area()
    {
    }

    public Area(AreaDimensions dimensions)
    {
        x = dimensions.x;
        y = dimensions.y;
    }
}
