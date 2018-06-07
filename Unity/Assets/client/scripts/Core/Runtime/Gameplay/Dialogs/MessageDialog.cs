using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class MessageDialog : Dialog
{
    public class MessageDialogResponse : Response
    {
        private Option _choice;
        public Option Choice { get { return _choice; } }

        public MessageDialogResponse(Option choice)
        {
            _choice = choice;
        }
    }

    [CreateAssetMenu(fileName = "MessageDialogConfig", menuName = "Config/Dialogs/MessageDialogConfig", order = 1)]
    [Serializable]
    public class MessageDialogConfig : Config
    {
        public delegate void OptionDelegate(Option selected);

        public event OptionDelegate OnOptionSelected;

        [SerializeField]
        private List<Option> _options;

        public List<Option> options { get { return _options; } }

        public MessageDialogConfig(List<Option> options)
        {
            _options = options;
        }
    }

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

    [SerializeField]
    private GameObject _button;
    [SerializeField]
    private Transform _buttonContainer;

    private MessageDialogConfig _config;
    private Option _selected;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        _config = config as MessageDialogConfig;
        base.Setup(config, responseDelegate);
        DisplayOptions(_config);
    }

    protected virtual void DisplayOptions(MessageDialogConfig config)
    {
        foreach(Option option in config.options)
        {
            GameObject button = Instantiate(_button);
            button.SetActive(true);
            button.transform.SetParent(_buttonContainer.transform, false);
            IOptionDisplay optionDisplay = button.GetComponent<IOptionDisplay>();
            optionDisplay.DisplayOption(option);

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnOptionSelected(option));
        }
    }

    protected void OnOptionSelected(Option option)
    {
        _selected = option;
        Close();
    }

    protected override Response GenerateResponse()
    {
        return new MessageDialogResponse(_selected);
    }

    public interface IOptionDisplay
    {
        void DisplayOption(Option option);
    }
}

