
using System.Collections.Generic;

public class GridDataPoint<TData>
{
    public TData Data;
    public INTVector Vector;

    public GridDataPoint()
    {
        
    }
    
    public GridDataPoint(TData data, INTVector vector)
    {
        Data = data;
        Vector = vector;
    }
}

public abstract class AbstractGridDataModel<TData, TGridDataPoint> where TGridDataPoint : GridDataPoint<TData>, new()
{
    private TData[,] _data;

    public readonly int Width;
    public readonly int Height;

    public AbstractGridDataModel(TData[,] data)
    {
        _data = data;
        Width = data.GetLength(0);
        Height = data.GetLength(1);
    }
    
    public AbstractGridDataModel(uint width, uint height)
    {
        _data = new TData[width, height];
    }

    public TData this[INTVector vector]
    {
        get { return _data[vector.X, vector.Y]; }
        set { _data[vector.X, vector.Y] = value; }
    }
    
    public TData this[int x, int y]
    {
        get { return _data[x, y]; }
        set { _data[x, y] = value; }
    }

    public IEnumerable<TGridDataPoint> AllData()
    {
        for (int x = 0; x < _data.GetLength(0); x++)
        {
            for (int y = 0; y < _data.GetLength(1); y++)
            {
                yield return new TGridDataPoint() {Data = _data[x, y], Vector = new INTVector(x, y)};
            }
        }
    }
}
