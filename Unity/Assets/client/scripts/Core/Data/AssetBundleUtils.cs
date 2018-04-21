using UnityEngine;
using System.Collections;
using System;

public class AssetBundleUtils
{
    public const string PathAssetBundleRoot = "Assets/AssetBundles/";
    private const string EmptyString = "";
    private const char PathDelimiter = '/';

    public static string GetBundleNameFromPath(string path)
    {
        string bundledPath = path.Replace(PathAssetBundleRoot, EmptyString);
        string[] splitBundlePath = bundledPath.Split(PathDelimiter);
        return splitBundlePath[0].ToLower();
    }
}
