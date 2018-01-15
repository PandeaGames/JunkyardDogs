using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System.Collections.Generic;
using UnityEditor;

namespace JunkyardDogs.Components
{
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
        private Weapon _topArmament;

        [SerializeField]
        private Weapon _frontArmament;

        [SerializeField]
        private Weapon _leftArmament;

        [SerializeField]
        private Weapon _rightArmament;

        public List<Plate> FrontPlates { get { return _frontPlates; } }
        public List<Plate> LeftPLates { get { return _leftPlates; } }
        public List<Plate> RightPlates { get { return _rightPlates; } }
        public List<Plate> BackPlates { get { return _backPlates; } }
        public List<Plate> BottomPlates { get { return _bottomPlates; } }

        public Weapon TopArmament { get { return _topArmament; } }
        public Weapon FrontArmament { get { return _frontArmament; } }
        public Weapon LeftArmament { get { return _leftArmament; } }
        public Weapon RightArmament { get { return _rightArmament; } }
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
            {
                DisplayPlates("Front Plates", specification.FrontPlates, chassis.FrontPlates);
                DisplayPlates("Left Plates", specification.LeftPLates, chassis.LeftPLates);
                DisplayPlates("Right Plates", specification.RightPlates, chassis.RightPlates);
                DisplayPlates("Back Plates", specification.BackPlates, chassis.BackPlates);
                DisplayPlates("Bottom Plates", specification.BottomPlates, chassis.BottomPlates);
            }
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