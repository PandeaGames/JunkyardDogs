using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bot Render Config")]
public class BotRenderConfiguration : ScriptableObject
{
    [SerializeField] 
    private PrefabFactory _componentFactory;
    
    [SerializeField] 
    private ComponentArtConfig _componentArtConfig;
    
    [SerializeField] 
    private ScriptableObjectFactory _avatarFactory;
    
    [SerializeField] 
    private MaterialFactory _materials;
    
    [SerializeField] 
    private Material _missingComponentMaterial;
    
    [SerializeField] 
    private PrefabFactory _botPrefabFactory;

    public ScriptableObjectFactory AvatarFactory
    {
        get { return _avatarFactory; }
    }
    
    public MaterialFactory Materials
    {
        get { return _materials; }
    }
    
    public Material MissingComponentMaterial
    {
        get { return _missingComponentMaterial; }
    }
    
    public PrefabFactory BotPrefabFactory
    {
        get { return _botPrefabFactory; }
    }
}
