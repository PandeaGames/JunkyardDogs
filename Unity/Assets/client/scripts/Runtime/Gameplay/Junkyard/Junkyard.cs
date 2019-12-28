
public class Junkyard
{
    private JunkyardData _junkyardData;
    private SerializedJunkyard _serializedJunkyard;

    public SerializedJunkyard serializedJunkyard
    {
        get { return _serializedJunkyard; }
    }
    
    public string ID
    {
        get { return _junkyardData.name; }
    }
    
    public Junkyard(JunkyardData junkyardData, SerializedJunkyard serializedJunkyard)
    {
        _junkyardData = junkyardData;
        _serializedJunkyard = serializedJunkyard;
    }

    public int Width
    {
        get { return _serializedJunkyard.Data.GetLength(0); }
    }
    
    public int Height
    {
        get { return _serializedJunkyard.Data.GetLength(1); }
    }
}
