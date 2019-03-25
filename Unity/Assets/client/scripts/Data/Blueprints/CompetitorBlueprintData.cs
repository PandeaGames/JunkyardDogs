﻿
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(menuName = "Blueprints/CompetitorBlueprint")]
public class CompetitorBlueprintData : BlueprintData<Competitor>, IStaticDataBalance<CompetitorBalanceObject>
{
    [SerializeField, StaticDataReference(path:NationalityDataProvider.FULL_PATH)]
    private NationalityStaticDataReference _nationality;
    
    [SerializeField, BotBlueprintStaticDataReference]
    private List<BotBlueprintStaticDataReference> _bots;

    public override Competitor DoGenerate(int seed)
    {
        Competitor competitor = new Competitor();

        int itemsToLoad = 0;
        
        competitor.Nationality = _nationality;
        
        _bots.ForEach((botBlueprint =>
        {
            itemsToLoad++;
            Bot bot = botBlueprint.Data.DoGenerate(seed);
            competitor.Inventory.Bots.Add(bot);
        }));

        return competitor;
    }

    public void ApplyBalance(CompetitorBalanceObject balance)
    {
        if (_nationality == null)
        {
            _nationality = new NationalityStaticDataReference();
        }
        
        _bots = new List<BotBlueprintStaticDataReference>();

        name = balance.name;
        _nationality.ID = balance.nationality;

        string[] bots = balance.bots.Split(',');

        for (int i = 0; i < bots.Length; i++)
        {
            var reference = new BotBlueprintStaticDataReference();
            reference.ID = bots[i];
            _bots.Add(reference);
        }
    }

    public CompetitorBalanceObject GetBalance()
    {
        if (_nationality == null)
        {
            _nationality = new NationalityStaticDataReference();
        }

        if (_bots == null)
        {
            _bots = new List<BotBlueprintStaticDataReference>();
        }

        List<string> botIDs = new List<string>();
        
        _bots.ForEach(reference => botIDs.Add(reference.ID));
        
        CompetitorBalanceObject balance = new CompetitorBalanceObject();

        balance.name = name;
        balance.bots = string.Join(",", botIDs);
        balance.nationality = _nationality.ID;

        return balance;
    }
}