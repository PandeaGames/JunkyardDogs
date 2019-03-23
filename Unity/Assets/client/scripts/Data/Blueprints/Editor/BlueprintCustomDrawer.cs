using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/*[CustomPropertyDrawer(typeof(BlueprintBase), true)]
public class BlueprintCustomDrawer : PropertyDrawer
{    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty blueprintBaseProperty = property.FindPropertyRelative("_blueprintBase");
        BlueprintData blueprintBaseData = blueprintBaseProperty.objectReferenceValue as BlueprintData;

        if (blueprintBaseData == null)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        else
        {
            blueprintBaseData = EditorGUI.ObjectField(position, label, blueprintBaseData, typeof(BlueprintData), false) as BlueprintData;
            blueprintBaseProperty.objectReferenceValue = blueprintBaseData;
        }
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty blueprintBaseProperty = property.FindPropertyRelative("_blueprintBase");
        BlueprintData blueprintBase = blueprintBaseProperty.objectReferenceValue as BlueprintData;

        if (blueprintBase == null)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
        else
        {
            return 16;
        }
    }
}*/