using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using WeakReference = Data.WeakReference;

/*
[AttributeUsage(AttributeTargets.Field)]
public class WeakReferenceAttribute : PropertyAttribute
{
    private Type _typeRestriction;
    public Type TypeRestriction
    {
        get
        {
            return _typeRestriction;
        }
    }

    public WeakReferenceAttribute()
    {
        _typeRestriction = typeof(ScriptableObject);
    }

    public WeakReferenceAttribute(Type typeRestriction)
    {
        _typeRestriction = typeRestriction;
    }
}*/

[CustomPropertyDrawer(typeof(WeakReference))]
public class WeakReferenceEditor : PropertyDrawer
{
    private Dictionary<SerializedProperty, ScriptableObject> _objectTable = new Dictionary<SerializedProperty, ScriptableObject>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ScriptableObject objectReference;

        _objectTable.TryGetValue(property, out objectReference);

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty guid = property.FindPropertyRelative("_guid");
        SerializedProperty path = property.FindPropertyRelative("_path");

        if(!string.IsNullOrEmpty(path.stringValue) && objectReference ==  null)
        {
            objectReference = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path.stringValue);
            _objectTable.Add(property, objectReference);
        }

        EditorGUI.BeginChangeCheck();
        objectReference = EditorGUI.ObjectField(position, objectReference, typeof(ScriptableObject),false) as ScriptableObject;

        if(EditorGUI.EndChangeCheck())
        {
            string assetPath = AssetDatabase.GetAssetPath(objectReference);
            guid.stringValue = AssetDatabase.AssetPathToGUID(assetPath);
            path.stringValue = assetPath;
        }

        EditorGUI.EndProperty();
    }
}
