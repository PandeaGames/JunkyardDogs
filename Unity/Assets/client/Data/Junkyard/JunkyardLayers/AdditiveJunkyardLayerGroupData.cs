using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/Layers/AdditiveJunkyardLayerGroupData")]
public class AdditiveJunkyardLayerGroupData : AbstractAdditiveJunkyardLayerData
{
    [SerializeField] 
    private AbstractJunkyardLayerData[] _layers;
    
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        foreach (AbstractJunkyardLayerData layer in _layers)
        {
            input = layer.Apply(input, seed);
        }
        
        return input;
    }
}
