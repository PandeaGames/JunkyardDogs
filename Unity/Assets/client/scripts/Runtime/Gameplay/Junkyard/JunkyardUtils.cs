
using UnityEngine;

public static class JunkyardUtils
{
    private const string HideJunkyardMeshsPropertyKey = "JunkyardUtils_HideJunkyardMeshsPropertyKey";
    private const string HideJunkyardWallsPropertyKey = "JunkyardUtils_HideJunkyardWallsPropertyKey";
    private const string HideJunkyardFogPropertyKey = "JunkyardUtils_HideJunkyardFogPropertyKey";
    private const string HideJunkyardGroundPropertyKey = "JunkyardUtils_HideJunkyardGroundPropertyKey";
    private const string HideJunkyardMiniMapPropertyKey = "JunkyardUtils_HideJunkyardMiniMapPropertyKey";
    
    public static bool HideJunkyardMeshs
    {
        get { return PlayerPrefs.GetInt(HideJunkyardMeshsPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardMeshsPropertyKey, value ? 1 : 0); }
    }
    public static bool HideWalls
    {
        get { return PlayerPrefs.GetInt(HideJunkyardWallsPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardWallsPropertyKey, value ? 1 : 0); }
    }
    
    public static bool HideFog
    {
        get { return PlayerPrefs.GetInt(HideJunkyardFogPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardFogPropertyKey, value ? 1 : 0); }
    }
    
    public static bool HideGround
    {
        get { return PlayerPrefs.GetInt(HideJunkyardGroundPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardGroundPropertyKey, value ? 1 : 0); }
    }
    
    public static bool HideMiniMap
    {
        get { return PlayerPrefs.GetInt(HideJunkyardMiniMapPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(HideJunkyardMiniMapPropertyKey, value ? 1 : 0); }
    }
}
