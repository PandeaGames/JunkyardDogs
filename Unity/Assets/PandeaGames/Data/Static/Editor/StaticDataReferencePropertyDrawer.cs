using System.Collections.Generic;
using PandeaGames.Data.Static;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StaticDataReferenceAttribute), useForChildren:true)]
public class StaticDataReferencePropertyDrawer : PropertyDrawer
{
    private string[] _options;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        StaticDataReferenceAttribute attr = attribute as StaticDataReferenceAttribute;

        if (attr.IDs.Length == 0)
        {
            Rect fieldEntryPosition = position;
            fieldEntryPosition.xMin = EditorGUIUtility.labelWidth + 15;
            
            EditorGUI.LabelField(position, label);
            EditorGUI.LabelField(fieldEntryPosition, "- no data available -");
        }
        else
        {
            List<GUIContent> options = new List<GUIContent>();
            SerializedProperty idProp = property.FindPropertyRelative("_id");

            int selectedIndex = 0;
            
            for (int i = 0; i < attr.IDs.Length; i++)
            {
                string id = attr.IDs[i];
                if (id.Equals(idProp.stringValue))
                {
                    selectedIndex = i;
                }
            }
            
            foreach (string id in attr.IDs)
            {
                options.Add(new GUIContent(id));
            }
            
            selectedIndex = EditorGUI.Popup(position ,label,selectedIndex, options.ToArray());

            idProp.stringValue = attr.IDs[selectedIndex];
            
            property.serializedObject.ApplyModifiedProperties();
        }
        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 16;
    }
}