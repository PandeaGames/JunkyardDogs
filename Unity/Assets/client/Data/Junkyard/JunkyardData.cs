using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardData")]
public class JunkyardData : ScriptableObject, IJunkyardGenerator
{
    [SerializeField] 
    private AbstractJunkyardLayerData[] _layers;
    
    [SerializeField]
    private AbstractJunkyardLayerData[] _heightMap;

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _seed;
    
    public SerializedJunkyard Generate()
    {
        return Generate(_seed);
    }
    
    public SerializedJunkyard Generate(int seed)
    {
        byte[,] data = new byte[_width,_height];
        return Generate(data, seed);
    }
    
    public SerializedJunkyard Generate(byte[,] input, int seed)
    {
        byte[,] heightData = new byte[_width,_height];
        
        foreach (AbstractJunkyardLayerData layer in _layers)
        {
            input = layer.Apply(input, seed);
        }
        
        foreach (AbstractJunkyardLayerData layer in _heightMap)
        {
            heightData = layer.Apply(heightData, seed);
        }

        SerializedJunkyard serializedData = new SerializedJunkyard();
        serializedData.Data = input;
        serializedData.HeightMap = heightData;
        serializedData.Cleared = new bool[input.GetLength(0),input.GetLength(1)];
        
        return serializedData;
    }

    public SerializedJunkyard Generate(int seed, int width, int height)
    {
        return Generate(new byte[width, height], seed);
    }
}