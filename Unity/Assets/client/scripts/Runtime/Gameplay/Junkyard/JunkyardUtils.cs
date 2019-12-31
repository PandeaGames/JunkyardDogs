
using UnityEngine;

public static class JunkyardUtils
{
    private const string HideJunkyardMeshsPropertyKey = "JunkyardUtils_HideJunkyardMeshsPropertyKey";
    public static bool HideJunkyardMeshs
    {
        get { return PlayerPrefs.GetInt(HideJunkyardMeshsPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardMeshsPropertyKey, value ? 1 : 0); }
    }
}
