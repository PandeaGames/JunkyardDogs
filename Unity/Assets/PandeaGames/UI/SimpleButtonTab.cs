using UnityEngine;
using UnityEngine.UI;

namespace PandeaGames.UI
{
    [RequireComponent(typeof(Button))]
    public class SimpleButtonTab : Tab
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            RequestFocus();
        }
        
        public override void Blur()
        {
            base.Blur();
            _button.interactable = true;
        }

        public override void Focus()
        {
            base.Focus();
            _button.interactable = false;
        }
    }
}