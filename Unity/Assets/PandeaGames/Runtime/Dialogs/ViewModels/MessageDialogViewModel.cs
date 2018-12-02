using PandeaGames.ViewModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PandeaGames.Runtime.Dialogs.ViewModels
{
    public class MessageDialogViewModel : AbstractDialogViewModel<MessageDialogViewModel>
    {
        [Serializable]
        public class Option
        {
            [SerializeField]
            private string _title;

            public string Title { get { return _title; } }

            public Option(string title)
            {
                _title = title;
            }
        }
        
        private Option _choice;
        public Option Choice { get { return _choice; } }

        public delegate void OptionDelegate(Option selected);

        public event OptionDelegate OnOptionSelected;

        private List<Option> _options;

        public List<Option> options { get { return _options; } }
        
        public void SetOptions(List<Option> options)
        {
            _options = options;
        }

        public void SelectOption(Option option)
        {
            _choice = option;
            if (OnOptionSelected != null)
                OnOptionSelected(option);
        }
    }
}