
using System.ComponentModel;

public class JunkyardSROptions
{
    
}

public partial class SROptions
{
    
    public const string JUNKYARD_CATEGORY = "JUNKYARD";
    
    // Default Value for property
    private float _hideJunkyardMeshs = 0.5f;
	
    // Options will be grouped by category
    [Category(JUNKYARD_CATEGORY)] 
    public bool HideJunkyardMeshs {
        get { return JunkyardUtils.HideJunkyardMeshs; }
        set { JunkyardUtils.HideJunkyardMeshs = value; }
    }
}