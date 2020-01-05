
public struct INTVector
{
    public int X;
    public int Y;
    
    public INTVector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator INTVector(int radius)
    {
        return new INTVector(radius, radius);
    }
}
