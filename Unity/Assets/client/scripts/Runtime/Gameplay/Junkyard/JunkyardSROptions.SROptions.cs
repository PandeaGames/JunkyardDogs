using System.ComponentModel;
using PandeaGames;

public partial class SROptions
{
    private const string JUNKYARD_RENDER_CATEGORY = "JUNKYARD RENDERING";
    private const string JUNKYARD_DATA_CATEGORY = "JUNKYARD DATA";
    
    // Default Value for property
    private float _hideJunkyardMeshs = 0.5f;
	
    // Options will be grouped by category
    [Category(JUNKYARD_RENDER_CATEGORY)] 
    public bool HideJunkyardMeshs {
        get { return JunkyardUtils.HideJunkyardMeshs; }
        set { JunkyardUtils.HideJunkyardMeshs = value; }
    }
    
    // Options will be grouped by category
    [Category(JUNKYARD_RENDER_CATEGORY)] 
    public bool HideJunkyardWalls {
        get { return JunkyardUtils.HideWalls; }
        set { JunkyardUtils.HideWalls = value; }
    }
    
    // Options will be grouped by category
    [Category(JUNKYARD_RENDER_CATEGORY)] 
    public bool HideJunkyardFog {
        get { return JunkyardUtils.HideFog; }
        set { JunkyardUtils.HideFog = value; }
    }
    
    // Options will be grouped by category
    [Category(JUNKYARD_RENDER_CATEGORY)] 
    public bool HideJunkyardGround {
        get { return JunkyardUtils.HideGround; }
        set { JunkyardUtils.HideGround = value; }
    }
    
    // Options will be grouped by category
    [Category(JUNKYARD_RENDER_CATEGORY)] 
    public bool HideJunkyardMiniMap {
        get { return JunkyardUtils.HideMiniMap; }
        set { JunkyardUtils.HideMiniMap = value; }
    }
    
    // Options will be grouped by category
    [Category(JUNKYARD_DATA_CATEGORY)] 
    public void DeleteCurrentJunkyardData()
    {
        Game.Instance.GetService<JunkyardService>().DeleteJunkyardData(
            Game.Instance.GetService<JunkyardUserService>().User.Junkard
            );
    } 
}