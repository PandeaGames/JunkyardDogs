using System;

[Serializable]
public class SerializedJunkyard
{
    public byte[,] HeightMap { get; set; }
    public byte[,] Data { get; set; }
    public bool[,] Cleared { get; set; }
}
