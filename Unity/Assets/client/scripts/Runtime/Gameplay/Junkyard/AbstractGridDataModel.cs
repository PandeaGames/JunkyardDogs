
using System;
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
    public delegate void DataHasChangedDelegate(IEnumerable<TGridDataPoint> data);

    public event DataHasChangedDelegate OnDataHasChanged;
    
    protected TData[,] _data {private set; get; }

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

    public virtual void UpdateData(INTVector vector)
    {
        
    }

    protected virtual void DataHasChanged(IEnumerable<TGridDataPoint> data)
    {
        OnDataHasChanged?.Invoke(data);
    }

    protected virtual IEnumerable<TGridDataPoint> GenerateVectorGrid(INTVector center, INTVector dimensions)
    {
        return GenerateVectorGrid(
            left: Math.Max(0, center.X - dimensions.X),
            top: Math.Max(0, center.Y - dimensions.Y),
            right: Math.Min(Width - 1, center.X + dimensions.X),
            bottom: Math.Min(Height - 1, center.Y + dimensions.Y));
    }
    
    protected virtual IEnumerable<TGridDataPoint> GenerateVectorGrid(int left, int top, int right, int bottom)
    {
        for (int x = left; x <= right; x++)
        {
            for (int y = top; y <= bottom; y++)
            {
                yield return new TGridDataPoint() {Data = _data[x, y], Vector = new INTVector(x, y)};
            }
        }
    }
}
