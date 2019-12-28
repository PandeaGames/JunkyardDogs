using UnityEngine;

[CreateAssetMenu(menuName = "Junkyard/Layers/EchoJunkyardLayerData")]
public class JunkyardEchoLayerData : AbstractAdditiveJunkyardLayerData
{ 
    protected override byte[,] GetAdditive(byte[,] input, int seed)
    {
        return input;
    }
}