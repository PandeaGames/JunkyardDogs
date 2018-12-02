
using Data;
using UnityEngine;
using System;
using System.Collections.Generic;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class CompetitorBlueprint : Blueprint<Competitor, CompetitorBlueprintData>
{
    [SerializeField, WeakReference(typeof(Nationality))]
    private WeakReference _nationality;

    [SerializeField]
    private List<BotBlueprint> _bots;

    protected override void DoGenerate(int seed, Action<Competitor> onComplete, Action onError)
    {
        Competitor competitor = new Competitor();

        int itemsToLoad = 0;
        
        competitor.Nationality = _nationality;
        
        _bots.ForEach((botBlueprint =>
        {
            itemsToLoad++;
            botBlueprint.Generate((bot) =>
            {
                competitor.Inventory.Bots.Add(bot);
                if (--itemsToLoad <= 0)
                {
                    onComplete(competitor);
                }
            }, onError);
        }));
    }
}
