using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using System;

namespace JunkyardDogs.Components
{
    [Serializable]
    public class Chassis : Component<Specifications.Chassis>
    {
        public List<Plate> FrontPlates { get; set; }

        [SerializeField]
        public List<Plate> LeftPlates { get; set; }

        [SerializeField]
        public List<Plate> RightPlates { get; set; }

        [SerializeField]
        public List<Plate> BackPlates { get; set; }

        [SerializeField]
        public List<Plate> TopPlates { get; set; }

        [SerializeField]
        public List<Plate> BottomPlates { get; set; }

        [SerializeField]
        public WeaponProcessor TopArmament { get; set; }

        [SerializeField]
        public WeaponProcessor FrontArmament { get; set; }

        [SerializeField]
        public WeaponProcessor LeftArmament { get; set; }

        [SerializeField]
        public WeaponProcessor RightArmament { get; set; }

        public Chassis()
        {

        }
    }
    /*
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
    }*/
}