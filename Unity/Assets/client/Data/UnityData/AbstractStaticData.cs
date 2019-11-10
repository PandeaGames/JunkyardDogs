using UnityEngine;

public class AbstractStaticData : ScriptableObject, IStaticData
{
    public string ID
    {
        get { return name; }
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }
}
