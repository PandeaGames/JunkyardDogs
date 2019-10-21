using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Bot Render Config")]
public class BotRenderConfiguration : ScriptableObject
{
    [SerializeField] 
    private PrefabFactory _componentFactory;
    
    [SerializeField] 
    private ScriptableObjectFactory _avatarFactory;
    
    [SerializeField] 
    private MaterialFactory _materials;

    [SerializeField] 
    public GameObject ProjectilePrefab;
    
    [SerializeField] 
    public GameObject PulsePrefab;
    
    [SerializeField] 
    public GameObject MeleePrefab;
    
    [SerializeField] 
    public GameObject LaserPrefab;
    
    [SerializeField] 
    public GameObject MachineGunPrefab;
    
    [SerializeField] 
    public GameObject ExplosionPrefab;
    
    [SerializeField] 
    public GameObject SmallExplosionPrefab;
    
    [SerializeField] 
    public GameObject SmallPlasmaExplosionPrefab;
    
    [SerializeField]
    public GameObject CameraDronePrefab;
    
    [SerializeField] 
    private Material _missingComponentMaterial;
    
    public PrefabFactory ComponentFactory
    {
        get { return _componentFactory; }
    }
    
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
}
