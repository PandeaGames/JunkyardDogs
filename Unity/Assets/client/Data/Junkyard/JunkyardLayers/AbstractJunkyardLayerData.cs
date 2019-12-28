using UnityEngine;

public abstract class AbstractJunkyardLayerData : ScriptableObject, IJunkyardGeneratorLayer
{
    public abstract byte[,] Apply(byte[,] input, int seed);
}