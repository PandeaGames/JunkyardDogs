
using Data;
using UnityEngine;
using System;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using PandeaGames.Data.Static;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class CompetitorBlueprint : Blueprint<Competitor, CompetitorBlueprintData>
{
    [SerializeField, StaticDataReference(path:NationalityDataProvider.FULL_PATH)]
    private NationalityStaticDataReference _nationality;
    
    [SerializeField]
    private List<BotBlueprint> _bots;

    protected override Competitor DoGenerate(int seed)
    {
        Competitor competitor = new Competitor();

        int itemsToLoad = 0;
        
        competitor.Nationality = _nationality;
        
        _bots.ForEach((botBlueprint =>
        {
            itemsToLoad++;
            Bot bot = botBlueprint.Generate();
                competitor.Inventory.Bots.Add(bot);
        }));

        return competitor;
    }
}
