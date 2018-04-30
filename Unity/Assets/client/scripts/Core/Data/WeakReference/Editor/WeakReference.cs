using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using WeakReference = Data.WeakReference;

[CustomPropertyDrawer(typeof(WeakReferenceAttribute))]
public class WeakReferenceAttribuiteEditor : PropertyDrawer
{
    private Dictionary<SerializedProperty, ScriptableObject> _objectTable = new Dictionary<SerializedProperty, ScriptableObject>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        WeakReferenceAttribute weakReferenceAttribute = attribute as WeakReferenceAttribute;

        ScriptableObject objectReference;

        _objectTable.TryGetValue(property, out objectReference);

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        SerializedProperty guid = property.FindPropertyRelative("_guid");
        SerializedProperty path = property.FindPropertyRelative("_path");

        if (!string.IsNullOrEmpty(path.stringValue) && objectReference == null)
        {
            objectReference = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path.stringValue);
            _objectTable.Add(property, objectReference);
        }

        EditorGUI.BeginChangeCheck();
        objectReference = EditorGUI.ObjectField(position, objectReference, weakReferenceAttribute.TypeRestriction, false) as ScriptableObject;

        if (EditorGUI.EndChangeCheck())
        {
            string assetPath = AssetDatabase.GetAssetPath(objectReference);
            guid.stringValue = AssetDatabase.AssetPathToGUID(assetPath);
            path.stringValue = assetPath;
        }

        EditorGUI.EndProperty();
    }
}