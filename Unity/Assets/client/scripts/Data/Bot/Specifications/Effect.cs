using UnityEngine;
using System.Collections;
namespace JunkyardDogs.Specifications
{
    public enum EffectType
    {
        DRAG,
        DISABLE,
        SLOW
    }

    public enum EffectStrength
    {
        LOW,
        MEDIUM,
        HIGH
    }

    [CreateAssetMenu(fileName = "Effect", menuName = "Specifications/Effect", order = 1)]
    public class Effect : ScriptableObject
    {
        [SerializeField]
        private EffectType _type;

        [SerializeField]
        private int _duration;

        [SerializeField]
        private EffectStrength _strength;

        public EffectType Type { get { return _type; } }
        public int Duration { get { return _duration; } }
        public EffectStrength Strength { get { return _strength; } }
    }
}