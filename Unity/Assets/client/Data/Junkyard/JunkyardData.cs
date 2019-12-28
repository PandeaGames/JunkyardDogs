using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardData")]
public class JunkyardData : ScriptableObject, IJunkyardGenerator
{
    [SerializeField] 
    private AbstractJunkyardLayerData[] _layers;

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _seed;
    
    public byte[,] Generate()
    {
        return Generate(_seed);
    }
    
    public byte[,] Generate(int seed)
    {
        byte[,] data = new byte[_width,_height];
        
        foreach (AbstractJunkyardLayerData layer in _layers)
        {
            data = layer.Apply(data, seed);
        }
        
        return Generate(data, seed);
    }
    
    public byte[,] Generate(byte[,] input, int seed)
    {
        foreach (AbstractJunkyardLayerData layer in _layers)
        {
            input = layer.Apply(input, seed);
        }
        
        return input;
    }

    public byte[,] Generate(int seed, int width, int height)
    {
        return Generate(new byte[width, height], seed);
    }
}