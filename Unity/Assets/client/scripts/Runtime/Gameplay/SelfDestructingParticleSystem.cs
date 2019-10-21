using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Simulation;
using UnityEngine;

public class SelfDestructingParticleSystem : MonoBehaviour
{
    private ParticleSystem _ps;

    // Start is called before the first frame update
    void Start()
    {
        _ps = FindObjectOfType<ParticleSystem>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_ps != null && !_ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
    
}
