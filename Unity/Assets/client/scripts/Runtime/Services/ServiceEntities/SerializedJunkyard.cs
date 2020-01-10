using System;

[Serializable]
public class SerializedJunkyard
{
    public byte[,] HeightMap { get; set; }
    public byte[,] Data { get; set; }
    public bool[,] Cleared { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Seed { get; set; }
}

