using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JunkyardView))]
public class JunkyardEditor : Editor
{
    private Texture2D _testTexture;

    [SerializeField]
    private JunkyardData _junkyardData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _junkyardData = (JunkyardData) EditorGUILayout.ObjectField(_junkyardData, typeof(JunkyardData), false);
        EditorGUI.BeginDisabledGroup(_junkyardData == null);
        if (GUILayout.Button("Generate Preview"))
        {
            _testTexture = GeneratePreview(_junkyardData);
        }
        EditorGUI.EndDisabledGroup();

        if (_testTexture != null)
        {
            EditorGUI.DrawPreviewTexture(new Rect(275, 60, _testTexture.width, _testTexture.height), _testTexture);
        }
    }

    private Texture2D GeneratePreview(JunkyardData junkyardData)
    {
        byte[,] data = junkyardData.Generate();
        
        Texture2D tex = new Texture2D(data.GetLength(0), data.GetLength(1));

        for (int x = 0; x < data.GetLength(0); x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                float value = data[x, y] / (float) byte.MaxValue;
                tex.SetPixel(x, y, new Color(value, value, value));
            }
        }
        
        tex.Apply();
        return tex;
    }
}
