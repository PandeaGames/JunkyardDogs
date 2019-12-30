using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JunkyardMonoView))]
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
        if (GUILayout.Button("Generate Data Preview"))
        {
            _testTexture = GenerateDataPreview(_junkyardData);
        }
        if (GUILayout.Button("Generate Height Preview"))
        {
            _testTexture = GenerateHeightDataPreview(_junkyardData);
        }
        EditorGUI.EndDisabledGroup();

        if (_testTexture != null)
        {
            EditorGUI.DrawPreviewTexture(new Rect(275, 60, _testTexture.width, _testTexture.height), _testTexture);
        }
    }

    private Texture2D GenerateDataPreview(JunkyardData junkyardData)
    {
        byte[,] data = junkyardData.Generate().Data;
        return GeneratePreview(data);
    }
    
    private Texture2D GenerateHeightDataPreview(JunkyardData junkyardData)
    {
        byte[,] data = junkyardData.Generate().HeightMap;
        return GeneratePreview(data);
    }
    
    private Texture2D GeneratePreview(byte[,] data)
    { 
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
