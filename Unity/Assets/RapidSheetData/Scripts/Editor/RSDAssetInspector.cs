/// 
/// File:				RSDAssetInspector.cs
/// 
/// System:				Rapid Sheet Data (RSD) Unity3D client library
/// Version:			1.0.0
/// 
/// Language:			C#
/// 
/// License:				
/// 
/// Author:				Tasos Giannakopoulos (tasosg@voidinspace.com)
/// Date:				08 Mar 2017
/// 
/// Description:		
/// 


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace Lib.RapidSheetData
{
    /// 
    /// Class:       RSDEditorUtils
    /// Description: 
    ///
    public static class RSDEditorUtils
    {
        /// <summary>
        /// Find all classes marked with the RSDObject attribute
        /// http://stackoverflow.com/questions/607178/how-enumerate-all-classes-with-custom-class-attribute#607204
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="types"></param>
        /// <param name="names"></param>
        public static void GetClassesWithAttribute(Type attribute, out List<Type> types, out string[] names)
        {
            types = (from a in AppDomain.CurrentDomain.GetAssemblies()
                                 from t in a.GetTypes()
                                 let attributes = t.GetCustomAttributes(attribute, true)
                                 where (attributes != null) && (attributes.Length > 0)
                                 select t).ToList();

            names = new string[types.Count];
            for (int idx = 0; idx < types.Count; ++idx)
            {
                names[idx] = types[idx].Name;
            }
        }
    }

    /// 
    /// Class:       RSDAssetInspector
    /// Description: 
    ///
    [CustomEditor(typeof(RSDAsset))]
    public class RSDAssetInspector : Editor
    {
        // 
        private ReorderableList _list = null;
        private List<Type> _rsdTargetClasses = null;
        private string[] _rsdTargetClassesString = null;

        /// <summary>
        /// 
        /// </summary>
        void OnEnable()
        {
            RSDEditorUtils.GetClassesWithAttribute(typeof(RSDObject), out _rsdTargetClasses, out _rsdTargetClassesString);

            _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_sheets"), true, true, true, true);

            _list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Sheets");
            };

            _list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = _list.serializedProperty.GetArrayElementAtIndex(index);

                rect.y += 2;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 170, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("_sheetName"), GUIContent.none);

                // Target class
                {
                    var targetClass = element.FindPropertyRelative("_targetClass");
                    int selection = 0;
                    for(int idx = 0; idx < _rsdTargetClasses.Count; ++idx)
                    {
                        if (targetClass.stringValue == _rsdTargetClasses[idx].AssemblyQualifiedName)
                        {
                            selection = idx;
                            break;
                        }
                    }

                    selection = EditorGUI.Popup(new Rect(rect.x + rect.width - 165, rect.y, 100, EditorGUIUtility.singleLineHeight),
                        selection,
                        _rsdTargetClassesString);

                    if((selection >= 0) && (selection < _rsdTargetClasses.Count))
                    {
                        targetClass.stringValue = _rsdTargetClasses[selection].AssemblyQualifiedName;
                    }
                }

                EditorGUI.PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("_majorDimension"), GUIContent.none);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            _list.DoLayoutList();

            // Fetches a sheet or sheets and stores them in the data asset
            if (GUILayout.Button("Pull data"))
            {
                var rsdAsset = target as RSDAsset;
                if(rsdAsset != null)
                {
                    rsdAsset.PullDataAndCache();
                    EditorUtility.SetDirty(target);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
} /// Lib.RapidSheetData