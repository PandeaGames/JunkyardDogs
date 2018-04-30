using UnityEngine;
using System.Collections;
using UnityEditor;

public class ServiceEditorTools
{
    public const string ServiceEditorMenu = "Services/";
    private const string ClearUserDataMenuItem = CoreEditorTools.CoreEditorToolsMenu + ServiceEditorMenu + "ClearUserData";

    [MenuItem(ClearUserDataMenuItem)]
    public static void ClearUserData()
    {
        UserServiceUtils.ClearUserData();
        EditorUtility.DisplayDialog("Success", "User Data Cleared", "OK");
    }
}
