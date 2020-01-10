
using System;
using System.Collections.Generic;
using UnityEngine;

public class Junkyard
{
    public delegate void DataPointUpdated(int x, int y, Junkyard junkyard);
    
    public event DataPointUpdated Update; 
    
    private JunkyardData _junkyardData;
    private SerializedJunkyard _serializedJunkyard;
    
    public JunkyardData.Reward[] Rewards { private set; get; }

    public SerializedJunkyard serializedJunkyard
    {
        get { return _serializedJunkyard; }
    }
    
    public string ID
    {
        get { return _junkyardData.name; }
    }
    
    public Junkyard(JunkyardData junkyardData, SerializedJunkyard serializedJunkyard)
    {
        _junkyardData = junkyardData;
        _serializedJunkyard = serializedJunkyard;
        Rewards = new List<JunkyardData.Reward>(junkyardData.GetRewards()).ToArray();
    }
    
    public float ChanceForSpecial
    {
        get { return _junkyardData.ChanceForSpecial; }
    }
    
    public int Width
    {
        get { return _serializedJunkyard.Data.GetLength(0); }
    }
    
    public int Height
    {
        get { return _serializedJunkyard.Data.GetLength(1); }
    }

    public int X
    {
        get { return _serializedJunkyard.X; }
        set { _serializedJunkyard.X = value; }
    }
    
    public int Y
    {
        get { return _serializedJunkyard.Y; }
        set { _serializedJunkyard.Y = value; }
    }

    public void SetCleared(int x, int y, bool value)
    {
        _serializedJunkyard.Cleared[x, y] = value;
        Update?.Invoke(x, y, this);
        
    }

    public bool GetCleared(int x, int y)
    {
        return _serializedJunkyard.Cleared[x, y];
    }

    public float GetHeight(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return _serializedJunkyard.Data[x, y] / (float) 100;
        }
        else
        {
            return 0;
        }
    }
    
    public float GetNormalizedHeight(int x, int y)
    {
        return
            (GetHeight(x, y) +
             GetHeight(x - 1, y - 1) +
             GetHeight(x, y - 1) +
             GetHeight(x + 1, y - 1) +
             GetHeight(x + 1, y) +
             GetHeight(x + 1, y + 1) +
             GetHeight(x, y + 1) +
             GetHeight(x - 1, y + 1) +
             GetHeight(x - 1, y)) / 9f;
    }

    public bool isAdjacentToCleared(int x, int y)
    {
        int totalSightDistance = 1;
        bool hasClearedAdjacent = false;
        bool hasClearedCloseAdjacent = false;
        for (int dx = x - totalSightDistance; dx <= x + totalSightDistance; dx++)
        {
            for (int dy = y - totalSightDistance; dy <= y + totalSightDistance; dy++)
            {
                if (dx > 0 && dx < Width && dy > 0 && dy < Height)
                {
                    bool isAdjacentCleared = serializedJunkyard.Cleared[dx, dy];

                    if (isAdjacentCleared)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    
    public Vector2 GetAdjacentToCleared(int x, int y)
    {
        int totalSightDistance = 1;
        bool hasClearedAdjacent = false;
        bool hasClearedCloseAdjacent = false;
        for (int dx = x - totalSightDistance; dx <= x + totalSightDistance; dx++)
        {
            for (int dy = y - totalSightDistance; dy <= y + totalSightDistance; dy++)
            {
                if (dx > 0 && dx < Width && dy > 0 && dy < Height)
                {
                    bool isAdjacentCleared = serializedJunkyard.Cleared[dx, dy];

                    if (isAdjacentCleared)
                    {
                        return new Vector2(dx, dy);
                    }
                }
            }
        }

        return new Vector2(x, y);
    }

    public IEnumerable<INTVector> GetGridSpaces()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                yield return new INTVector(x, y);
            }  
        }
    }
}
