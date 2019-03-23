
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/CompetitorBlueprint")]
public class CompetitorBlueprintData : BlueprintData<Competitor>
{
    [SerializeField, StaticDataReference(path:NationalityDataProvider.FULL_PATH)]
    private NationalityStaticDataReference _nationality;
    
    [SerializeField]
    private List<BotBlueprintData> _bots;

    public override Competitor DoGenerate(int seed)
    {
        Competitor competitor = new Competitor();

        int itemsToLoad = 0;
        
        competitor.Nationality = _nationality;
        
        _bots.ForEach((botBlueprint =>
        {
            itemsToLoad++;
            Bot bot = botBlueprint.DoGenerate(seed);
            competitor.Inventory.Bots.Add(bot);
        }));

        return competitor;
    }
}
