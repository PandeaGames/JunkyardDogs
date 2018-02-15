using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using UnityEditor;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    [CreateAssetMenu(fileName = "Chassis", menuName = "Components/Chassis", order = 3)]
    public class Chassis : Component<Specifications.Chassis>
    {
        [SerializeField]
        private List<Plate> _frontPlates = new List<Plate>();

        [SerializeField]
        private List<Plate> _leftPlates = new List<Plate>();

        [SerializeField]
        private List<Plate> _rightPlates = new List<Plate>();

        [SerializeField]
        private List<Plate> _backPlates = new List<Plate>();

        [SerializeField]
        private List<Plate> _topPlates = new List<Plate>();

        [SerializeField]
        private List<Plate> _bottomPlates = new List<Plate>();

        [SerializeField]
        private WeaponProcessor _topArmament;

        [SerializeField]
        private WeaponProcessor _frontArmament;

        [SerializeField]
        private WeaponProcessor _leftArmament;

        [SerializeField]
        private WeaponProcessor _rightArmament;

        public List<Plate> FrontPlates { get { return _frontPlates; } }
        public List<Plate> LeftPLates { get { return _leftPlates; } }
        public List<Plate> RightPlates { get { return _rightPlates; } }
        public List<Plate> BackPlates { get { return _backPlates; } }
        public List<Plate> BottomPlates { get { return _bottomPlates; } }

        public WeaponProcessor TopArmament { get { return _topArmament; } set { _topArmament = value; } }
        public WeaponProcessor FrontArmament { get { return _frontArmament; } set { _frontArmament = value; } }
        public WeaponProcessor LeftArmament { get { return _leftArmament; } set { _leftArmament = value; } }
        public WeaponProcessor RightArmament { get { return _rightArmament; } set { _rightArmament = value; } }
    }

    [CustomEditor(typeof(Chassis))]
    public class LevelScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Chassis chassis = (Chassis)target;
            chassis.Specification = (Specifications.Chassis)EditorGUILayout.ObjectField(chassis.Specification, typeof(Specifications.Chassis), false);

            Specifications.Chassis specification = chassis.Specification;

            if (specification)
                RenderChassis(chassis, specification);

            EditorUtility.SetDirty(chassis);
        }

        private void RenderChassis(Chassis chassis, Specifications.Chassis specification)
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Plating");

            GUILayout.Space(10);
            EditorGUI.indentLevel++;

            DisplayPlates("Front Plates", specification.FrontPlates, chassis.FrontPlates);
            DisplayPlates("Left Plates", specification.LeftPLates, chassis.LeftPLates);
            DisplayPlates("Right Plates", specification.RightPlates, chassis.RightPlates);
            DisplayPlates("Back Plates", specification.BackPlates, chassis.BackPlates);
            DisplayPlates("Bottom Plates", specification.BottomPlates, chassis.BottomPlates);

            GUILayout.Space(10);
            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Armament");

            GUILayout.Space(10);
            EditorGUI.indentLevel++;

            chassis.TopArmament =  DisplayArmament("Top Armament", specification.TopArmament, chassis.TopArmament);
            chassis.FrontArmament = DisplayArmament("Front Armament", specification.FrontArmament, chassis.FrontArmament);
            chassis.LeftArmament = DisplayArmament("Left Armament", specification.LeftArmament, chassis.LeftArmament);
            chassis.RightArmament = DisplayArmament("Right Armament", specification.RightArmament, chassis.RightArmament);

            EditorGUI.indentLevel--;
        }

        private WeaponProcessor DisplayArmament(string label, bool supports, WeaponProcessor current)
        {
            if (!supports)
                return null;

            EditorGUILayout.LabelField(label);

            WeaponProcessor subProcessor = (WeaponProcessor)EditorGUILayout.ObjectField(current, typeof(WeaponProcessor), false);

            if(subProcessor != null)
            {
                Editor editor = Editor.CreateEditor(subProcessor);
                editor.OnInspectorGUI();
                EditorUtility.SetDirty(subProcessor);
                GUILayout.Space(10);
            }

            return subProcessor;
        }

        private void DisplayPlates(string label, int count, List<Plate> list)
        {
            if (count == 0)
                return;

            list.Capacity = count;

            EditorGUILayout.LabelField(label);

            for (int i = 0; i < count; i++)
            {
                if (list.Count <= i)
                    list.Add(null);

                Plate toSet = (Plate)EditorGUILayout.ObjectField(list[i], typeof(Plate), false);

                if (toSet != null && !list.Contains(toSet))
                    list[i] = toSet;
            }
        }
    }
}