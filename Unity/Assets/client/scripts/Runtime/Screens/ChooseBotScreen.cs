﻿
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using PandeaGames.Views.Screens;

public class ChooseBotScreen : ScreenView
{
    public interface IChooseBotModel
    {
        void Focus(Bot bot);
        void Select(Bot bot);
    }

    [SerializeField]
    private Button _leftBtn;
    [SerializeField]
    private Button _rightBtn;
    [SerializeField]
    private Button _chooseBtn;
    
    private IChooseBotModel _model;
    private List<Bot> _bots;
    private int _index;
    
    public override void Setup(WindowView window)
    {
        base.Setup(window);

        /*_chooseBotConfig = config as ChooseBotConfig;
        _model = _chooseBotConfig.Model;
        _bots = _chooseBotConfig.Bots;*/
        
        _leftBtn.onClick.AddListener(OnLeftBtn);
        _rightBtn.onClick.AddListener(OnRightBtn);
        _chooseBtn.onClick.AddListener(OnChooseBtn);
    }

    private void OnLeftBtn()
    {
        _index -= _index > 0 ? 1:0;
        _model.Focus(_bots[_index]);
    }
    
    private void OnRightBtn()
    {
        _index += _index < _bots.Count - 1 ? 1:0;
        _model.Focus(_bots[_index]);
    }
    
    private void OnChooseBtn()
    {
        _model.Select(_bots[_index]);
    }
}
