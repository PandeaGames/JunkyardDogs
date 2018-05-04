using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using WeakReference = Data.WeakReference;

[Serializable]
public class ViewUserDataWindow : EditorWindow
{
    public const string SERVICE_ASSET = "Assets/Client/Editor/Services/ClientEditorServiceManager.asset";
    public const string SERVICE_ASSET_SERVICE = "Assets/Client/Editor/Services/PandeaUserService.asset";

    [MenuItem("PandeaGames/User/View User Data")]
    public static void OnLaunchUserDataWindow()
    {
        // Get existing open window or if none, make a new one:
        ViewUserDataWindow window = (ViewUserDataWindow)EditorWindow.GetWindow(typeof(ViewUserDataWindow));
        window.InitWindow();
        window.Show();
    }

    public void InitWindow()
    {
        LoadUser();
    }

    [SerializeField]
    public JunkyardUser _user;

    private void LoadUser()
    {
        _user = UserServiceUtils.Load<JunkyardUser>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Reload"))
            OnReloadData();
        if (GUILayout.Button("Clear User Data"))
            OnClearData();
        if (GUILayout.Button("Save User Data"))
            OnSaveData();

        if (_user != null)
        {
            OnUserGUI(_user);
        }
    }

    private void OnUserGUI(JunkyardUser user)
    {
        Competitor competitor = user.Competitor;
        WeakReference nationalityReference = competitor.Nationality;

        Nationality nationality = null;

        if (nationalityReference != null)
        {
            nationality = nationalityReference.Load<Nationality>();
        }

        user.UID = EditorGUI.TextField(EditorGUILayout.GetControlRect(), new GUIContent("UID"), _user.UID);
        user.Cash = EditorGUI.IntField(EditorGUILayout.GetControlRect(), new GUIContent("Cash"), _user.Cash);
        nationality = EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), new GUIContent("Nationality"), nationality, typeof(Nationality), false) as Nationality;

        if (nationalityReference != null)
        {
            nationalityReference.Asset = nationality;
        }
    }

    private void OnReloadData()
    {
        _user = UserServiceUtils.Load<JunkyardUser>();
        EditorUtility.DisplayDialog("Success", "User Succesfuly Loaded: " + _user.UID, "ok");
    }

    private void OnSaveData()
    {
        UserServiceUtils.Save(_user);
        EditorUtility.DisplayDialog("Success", "User Succesfuly saved: " + _user.UID, "ok");
    }

    private void OnClearData()
    {
        UserServiceUtils.ClearUserData();
        EditorUtility.DisplayDialog("Success", "User data cleared.", "ok");
    }
}
