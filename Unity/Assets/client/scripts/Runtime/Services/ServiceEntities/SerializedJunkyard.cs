using System;

[Serializable]
public class SerializedJunkyard
{
    public byte[,] Data { get; set; }
    public bool[,] Cleared { get; set; }
}
