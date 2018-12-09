using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Views;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using UnityEngine;

public class MatchViewController : AbstractViewController 
{
    protected override IView CreateView()
    {
        //TODO Load the bots
        return new MatchView();
    }
}
