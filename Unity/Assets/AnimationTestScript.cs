using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] 
    private float _progress;


    [SerializeField] private AbstractProgressDisplay _progressDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _progressDisplay.SetProgress(_progress);
    }
}
