using UnityEngine;
using System.Collections;

public class GameBootstrap : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Use this for initialization
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
