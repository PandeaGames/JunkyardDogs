using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/JunkyardData")]
public class JunkyardData : ScriptableObject, IJunkyardGenerator
{
    [Serializable]
    public struct Cropping
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [Serializable]
    public struct Reward
    {
        [WeightedLootCrateStaticDataReferenceAttribute]
        public LootCrateStaticDataReference crate;
    }
    
    [SerializeField] 
    private AbstractJunkyardLayerData[] _layers;
    
    [SerializeField]
    private AbstractJunkyardLayerData[] _heightMap;

    [SerializeField] 
    private Reward[] _rewards;
    public Reward[] Rewards { get => _rewards; }

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _entranceX;
    [SerializeField]
    private int _entranceY;
    [SerializeField]
    private int _seed;
    [SerializeField]
    public Cropping _cropping;
    
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
        serializedData.X = _entranceX;
        serializedData.Y = _entranceY;

        ApplyClearCropping(serializedData.Cleared, _cropping);
        
        return serializedData;
    }

    public SerializedJunkyard Generate(int seed, int width, int height)
    {
        return Generate(new byte[width, height], seed);
    }

    private void ApplyClearCropping(bool[,] data, Cropping cropping)
    {
        int width = data.GetLength(0);
        int height = data.GetLength(1);
        
        for (int x = 0; x < cropping.Left; x++)
        {
            for (int y = 0; y < height; y++)
            {
                data[x, y] = true;
            }
        }
        
        for (int x = width - cropping.Right; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                data[x, y] = true;
            }
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < cropping.Top; y++)
            {
                data[x, y] = true;
            }
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int y = height - cropping.Bottom; y < height; y++)
            {
                data[x, y] = true;
            }
        }
    }

    public IEnumerable<Reward> GetRewards()
    {
        foreach (Reward reward in _rewards)
        {
            yield return reward;
        }
    }
}