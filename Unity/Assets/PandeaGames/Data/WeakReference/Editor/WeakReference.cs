using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using PandeaGames.Data.WeakReferences;

[CustomPropertyDrawer(typeof(WeakReferenceAttribute))]
public class WeakReferenceAttribuiteEditor : PropertyDrawer
{
    private Dictionary<SerializedProperty, UnityEngine.Object> _objectTable = new Dictionary<SerializedProperty, UnityEngine.Object>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        WeakReferenceAttribute weakReferenceAttribute = attribute as WeakReferenceAttribute;

        UnityEngine.Object objectReference;

        _objectTable.TryGetValue(property, out objectReference);

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        SerializedProperty guid = property.FindPropertyRelative("_guid");
        SerializedProperty path = property.FindPropertyRelative("_path");

        
        if (path != null && !string.IsNullOrEmpty(path.stringValue) && objectReference == null)
        {
            objectReference = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path.stringValue);
            _objectTable.Add(property, objectReference);
        }

        EditorGUI.BeginChangeCheck();
        objectReference = EditorGUI.ObjectField(position, objectReference, weakReferenceAttribute.TypeRestriction, false);
        bool endChangeCheck = EditorGUI.EndChangeCheck();
        
        if (endChangeCheck)
        {
            string assetPath = AssetDatabase.GetAssetPath(objectReference);
            guid.stringValue = AssetDatabase.AssetPathToGUID(assetPath);
            path.stringValue = assetPath;
        }
        
        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }
}