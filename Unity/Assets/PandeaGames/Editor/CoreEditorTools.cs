using UnityEngine;
using System.Collections;
using UnityEditor;

public class CoreEditorTools
{
    public const string CoreEditorToolsMenu = "CoreEditorTools/";

    private const string TemplateImporterMenuItem = CoreEditorToolsMenu + "TemplateImporter";
    private const string ScreenManagerMenuItem = CoreEditorToolsMenu + "ScreenManager";

    private static void OpenWindow<T>() where T: EditorWindow
    {
        T window = (T)EditorWindow.GetWindow(typeof(T));
        window.Show();
    }

    private static void OpenUtility<T>() where T : EditorWindow
    {
        T window = (T)EditorWindow.GetWindow(typeof(T));
        window.ShowUtility();
    }

    [MenuItem(TemplateImporterMenuItem)]
    public static void OpenTemplateImporter()
    {
        OpenWindow<TemplateImporter>();
    }

    [MenuItem(ScreenManagerMenuItem)]
    public static void OpenScreenManager()
    {
        OpenWindow<ScreenManagerWindow>();
    }
}