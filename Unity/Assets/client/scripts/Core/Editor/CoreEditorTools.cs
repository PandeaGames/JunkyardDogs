using UnityEngine;
using System.Collections;
using UnityEditor;

public class CoreEditorTools
{
    private static void OpenWindow<T>() where T: EditorWindow
    {
        T window = (T)EditorWindow.GetWindow(typeof(T));
        window.Show();
    }

    [MenuItem("CoreEditorTools/TemplateImporter")]
    public static void OpenTemplateImporter()
    {
        OpenWindow<TemplateImporter>();
    }
}
