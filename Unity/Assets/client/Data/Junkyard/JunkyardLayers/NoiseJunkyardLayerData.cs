using System;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Junkyard/Layers/NoiseJunkyardLayerData")]
public class NoiseJunkyardLayerData : AbstractAdditiveJunkyardLayerData
{
    [SerializeField]
    private byte _min;
    
    [SerializeField]
    private byte _max;
    
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        Random random = new Random(seed);
        for (int x = 0; x < input.GetLength(0); x++)
        {
            for (int y = 0; y < input.GetLength(1); y++)
            {
                input[x, y] = (byte) random.Next(_min, _max);
            }
        }

        return input;
    }
}
