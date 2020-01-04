
public abstract class AbstractGridDataModel<TData>
{
    private TData[,] _data;

    public AbstractGridDataModel(TData[,] data)
    {
        _data = data;
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
}
