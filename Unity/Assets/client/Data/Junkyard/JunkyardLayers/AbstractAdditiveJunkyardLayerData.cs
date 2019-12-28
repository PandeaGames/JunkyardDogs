using System;
using UnityEngine;

public abstract class AbstractAdditiveJunkyardLayerData : AbstractJunkyardLayerData
{
    [SerializeField] 
    private byte _selectionMax = Byte.MaxValue;
    [SerializeField]
    private byte _selectionMin = Byte.MinValue;

    [SerializeField]
    private AbstractJunkyardLayerData _applicationLayer;
    
    public sealed override byte[,] Apply(byte[,] input, int seed)
    {
        byte[,] additiveData = GetAdditive((byte[,])input.Clone(), seed);
        
        //TODO: Apply additive values. Apply the _applicationLayer and then apply the values that match _max and _min
        int inputWidth = input.GetLength(0);
        int inputHeight = input.GetLength(1);

        for (int x = 0; x < inputWidth; x++)
        {
            for (int y = 0; y < inputHeight; y++)
            {
                if (additiveData[x, y] > _selectionMin && additiveData[x, y] < _selectionMax)
                {
                    input[x, y] = additiveData[x, y];
                }
            }
        }

        return input;
    }

    protected abstract byte[,] GetAdditive(byte[,] input, int seed);
}
