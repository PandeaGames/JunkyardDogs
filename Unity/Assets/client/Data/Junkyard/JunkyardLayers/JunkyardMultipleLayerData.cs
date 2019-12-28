using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/Layers/MultiplyJunkyardLayerData")]
public class JunkyardMultipleLayerData : AbstractAdditiveJunkyardLayerData
{
    [SerializeField]
    private float _multiplier;
    
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        //TODO: Multiply all values
        return input;
    }
}