using JunkyardDogs.Specifications;
using UnityEngine;

[CreateAssetMenu]
public class JunkyardDogs3DFactory : PrefabFactory
{
    [SerializeField] 
    private GameObject _projectile;
    
    [SerializeField] 
    private GameObject _melee;
    
    [SerializeField] 
    private GameObject _hitscan;
    
    [SerializeField] 
    private GameObject _pulseEmitter;
    
    public override GameObject GetAsset(ScriptableObject obj)
    {
        if (HasAsset(obj))
        {
            return base.GetAsset(obj);
        }
        else
        {
            if (obj is ProjectileWeapon)
            {
                return _projectile;
            }
            else if (obj is Melee)
            {
                return _melee;
            }
            else if (obj is Hitscan)
            {
                return _hitscan;
            }
            else if (obj is PulseEmitter)
            {
                return _pulseEmitter;
            }
        }
        
        return base.GetAsset(obj);
    }
}
