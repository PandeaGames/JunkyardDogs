using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/Layers/DeviceJunkyardLayerData")]
public class JunkyardDevideLayerData : AbstractAdditiveJunkyardLayerData
{
    [SerializeField]
    private float _devisor;
    
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        //TODO: Devide all values
        return input;
    }
}