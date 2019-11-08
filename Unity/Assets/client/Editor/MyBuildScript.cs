#if UNITY_IOS

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class MyBuildPostprocess
{
    [PostProcessBuild(999)]
    public static void OnPostProcessBuild( BuildTarget buildTarget, string path)
    {
        if(buildTarget == BuildTarget.iOS)
        {
            string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            Debug.LogFormat("iOS Post Process Build at project path '{0}'", projectPath);

            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromFile(projectPath);

            string target = pbxProject.TargetGuidByName("Unity-iPhone");            
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            pbxProject.WriteToFile (projectPath);
            Debug.Log("iOS Post Process Build Complete");
        }
    }
}

#endif