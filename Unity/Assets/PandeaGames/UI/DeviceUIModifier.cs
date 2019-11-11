using System;
using UnityEngine;
using UnityEngine.UI;

namespace PandeaGames.UI
{
    public class DeviceUIModifier :  MonoBehaviour
    {
        [Serializable]
        public struct UIModifier
        {
            public CanvasScaler canvasScaler;
            public float canvasScale;
        }
        
        [SerializeField, NaughtyAttributes.Label("DPI")]
        private bool _shouldModifyDPi;
        [SerializeField, NaughtyAttributes.ShowIf("ShouldShowDPI"), NaughtyAttributes.BoxGroup("DPI")]
        private float _dpi;
        [SerializeField, NaughtyAttributes.ShowIf("ShouldShowDPI"), NaughtyAttributes.BoxGroup("DPI")]
        private UIModifier _dpiModifier;

        private bool ShouldShowDPI()
        {
            return _shouldModifyDPi;
        }

        private void Start()
        {
            if (_shouldModifyDPi)
            {
                ApplyModifers(_dpiModifier);
            }
        }

        private void ApplyModifers(UIModifier modifier)
        {
            if (modifier.canvasScaler != null && Screen.dpi >= _dpi)
            {
                modifier.canvasScaler.scaleFactor = modifier.canvasScale;
            }
        }
    }
}