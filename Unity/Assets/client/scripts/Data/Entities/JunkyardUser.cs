using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using System;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using Component = JunkyardDogs.Components.Component;

[Serializable]
public class JunkyardUser : User, ILootCrateConsumer, IExperienceModel
{
    [SerializeField]
    private Competitor _competitor;

    [SerializeField]
    private Experience _experience;
    
    [SerializeField]
    private Wallet _wallet;
    
    [SerializeField]
    private Tournaments _tournaments;
    
    public Wallet Wallet { get { return _wallet; } set { _wallet = value; }}
    public Competitor Competitor { get { return _competitor; } set { _competitor = value; } }
    public Tournaments Tournaments { get { return _tournaments; } set { _tournaments = value; } }
    public Experience Experience  { get { return _experience; } set { _experience = value; } }

    public void AddComponent(Component component)
    {
        Competitor.Inventory.AddComponent(component);
    }

    public JunkyardUser()
    {
        Competitor = new Competitor();
        Tournaments = new Tournaments();
        Experience = new Experience();
        Wallet = new Wallet();
    }
    
    public void Consume(AbstractLootCrateData crateData, int seed)
    {
        LootDataModel LootDataModel = new LootDataModel(this, seed);
        Consume(crateData.GetLoot(LootDataModel), seed);
    }

    public void Consume(ILoot[] crateContents, int seed)
    {
        for (int i = 0; i < crateContents.Length; i++)
        {
            ILoot loot = crateContents[i];

            if (loot is Currency)
            {
                Consume(loot as Currency);
            }
            else if (loot is EngineBlueprintData)
            {
                Consume(loot as EngineBlueprintData, seed);
            }
            else if(loot is WeaponBlueprintData)
            {
                Consume(loot as WeaponBlueprintData, seed);
            }
            else if(loot is WeaponProcessorBlueprintData)
            {
                Consume(loot as WeaponProcessorBlueprintData, seed);
            }
            else if(loot is ChassisBlueprintData)
            {
                Consume(loot as ChassisBlueprintData, seed);
            }
            else if(loot is PlateBlueprintData)
            {
                Consume(loot as PlateBlueprintData, seed);
            }
            else if(loot is MotherboardBlueprintData)
            {
                Consume(loot as MotherboardBlueprintData, seed);
            }
            else if(loot is BotBlueprintData)
            {
                Consume(loot as BotBlueprintData, seed);
            }
            else if(loot is CPUBlueprintData)
            {
                Consume(loot as CPUBlueprintData, seed);
            }
            else if(loot is DirectiveBlueprintData)
            {
                Consume(loot as DirectiveBlueprintData, seed);
            }
            else
            {
                throw new NotSupportedException(string.Format("This type of loot [{0}] is not supported by this consumer. ", loot));
            }
        }
    }

    private void Consume(Currency currency)
    {
        Wallet.Add(currency);
    }

    private void Consume(EngineBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(WeaponBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(WeaponProcessorBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(ChassisBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(PlateBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(MotherboardBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(BotBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddBot(blueprint.DoGenerate(seed));
    }
    
    private void Consume(CPUBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }
    
    private void Consume(DirectiveBlueprintData blueprint, int seed)
    {
        Competitor.Inventory.AddComponent(blueprint.DoGenerate(seed));
    }

    public int GetExp(Nationality nationality)
    {
        return Experience.GetExp(nationality);
    }

    public uint GetLevel(Nationality nationality)
    {
        return Experience.GetLevel(nationality);
    }

    public void AddExp(Nationality nationality, int amount)
    {
        Experience.AddExp(nationality, amount);
    }

    public void Ascend(Nationality nationality)
    {
        Experience.Ascend(nationality);
    }

    public int GetTotalExp()
    {
        return Experience.GetTotalExp();
    }
    
    public TournamentStatus GetTournamentStatus(Tournament tournament)
    {
        TournamentMetaState meta;
        Tournaments.TryGetTournamentMeta(tournament, out meta);

        if (meta == null)
        {
            return TournamentStatus.Locked;
        }

        uint level = GetLevel(tournament.nation); 
        bool hasEnoughExperience = level >= tournament.nationalExpUnlockBreakpoint;

        if (hasEnoughExperience)
        {
            if (meta.Completions > 0)
            {
                return TournamentStatus.Available; 
            }
            else
            {
                return TournamentStatus.New;
            }
        }
        else
        {
            return TournamentStatus.Locked;
        }
    }
}