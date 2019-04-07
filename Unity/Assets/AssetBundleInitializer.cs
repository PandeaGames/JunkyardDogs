using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleInitializer : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
	    Debug.Log("AssetBundleInitializer");
	    AssetBundleManager.Initialize();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
