using UnityEngine;
using System.Collections;
using UnityEditor;

public class CoreEditorTools
{
    public const string CoreEditorToolsMenu = "CoreEditorTools/";

    private const string TemplateImporterMenuItem = CoreEditorToolsMenu + "TemplateImporter";

    private static void OpenWindow<T>() where T: EditorWindow
    {
        T window = (T)EditorWindow.GetWindow(typeof(T));
        window.Show();
    }

    [MenuItem(TemplateImporterMenuItem)]
    public static void OpenTemplateImporter()
    {
        OpenWindow<TemplateImporter>();
    }
}
