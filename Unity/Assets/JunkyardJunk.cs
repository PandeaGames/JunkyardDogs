using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkyardJunk : MonoBehaviour
{
    public event Action<int, int> OnClicked;
    private int x;
    private int y;
    
    public void Setup(int x, int y)
    {
        this.x = x;
        this.y = y;

    }

    private void OnMouseUp()
    {
        if (OnClicked != null)
        {
            OnClicked(x, y);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
