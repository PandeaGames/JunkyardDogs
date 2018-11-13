using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManagerInit : Service {

    public override void StartService(ServiceManager serviceManager)
    {
        base.StartService(serviceManager);

        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        var request = AssetBundleManager.Initialize();
        if (request != null)
            yield return StartCoroutine(request);
    }
}
