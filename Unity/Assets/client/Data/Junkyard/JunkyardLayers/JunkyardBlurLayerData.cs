using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/Layers/JunkyardBlurLayerData")]
public class JunkyardBlurLayerData : AbstractAdditiveJunkyardLayerData
{
    [SerializeField]
    private int _range;
    
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        int w = _range * 2 + 1;
        int h = _range * 2 + 1;
        int m = _range + 1;
        byte[,] original = (byte[,])input.Clone();
        int inputWidth = input.GetLength(0);
        int inputHeight = input.GetLength(1);
        int area = (_range + 2) * (_range + 2);
        
        for (int x = 0; x < inputWidth; x++)
        {
            for (int y = 0; y < inputHeight; y++)
            {
                int value = 0;

                for (int wx = -_range; wx <= _range; wx++)
                {
                    for (int wy = -_range; wy <= _range; wy++)
                    {
                        int xselection = x + wx;
                        int yselection = y + wy;

                        if (xselection >=0 &&
                            xselection < inputWidth &&
                            yselection >=0 &&
                            yselection <inputHeight)
                        {
                            value += original[xselection, yselection];
                        }
                    }
                }

                input[x, y] = (byte)((float)value / (float)area);
            }
        }
        
        return input;
    }
}