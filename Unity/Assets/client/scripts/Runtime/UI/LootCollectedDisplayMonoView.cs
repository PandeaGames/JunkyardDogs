using System;
using UnityEngine;

public class LootCollectedDisplayMonoView : LootMonoView
{
    [SerializeField] public float _lifetimeSeconds;
    
    private double _timeAtRender;
    
    public override void RenderLoot(ILoot loot)
    {
        base.RenderLoot(loot);
        _timeAtRender = Time.time;
    }

    private void Update()
    {
        if (_timeAtRender + _lifetimeSeconds < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
