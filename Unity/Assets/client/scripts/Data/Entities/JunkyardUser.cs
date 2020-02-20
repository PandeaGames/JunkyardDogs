using UnityEngine;
using JunkyardDogs.Components;
using System;
using JunkyardDogs.Data;

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

    [SerializeField, JunkyardStaticDataReference]
    private JunkyardStaticDataReference _junkard;
    
    public Wallet Wallet { get { return _wallet; } set { _wallet = value; }}
    public Competitor Competitor { get { return _competitor; } set { _competitor = value; } }
    public Tournaments Tournaments { get { return _tournaments; } set { _tournaments = value; } }
    public Experience Experience  { get { return _experience; } set { _experience = value; } }
    public JunkyardStaticDataReference Junkard  { get { return _junkard; } set { _junkard = value; } }

    public void AddComponent(IComponent component)
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
    
    public IConsumable[] Consume(AbstractLootCrateData crateData, int seed)
    {
        LootDataModel LootDataModel = new LootDataModel(this, seed);
        ILoot[] loot = crateData.GetLoot(LootDataModel);
        IConsumable[] consumables = Consume(loot, seed);
        return consumables;
    }

    public void Consume(IConsumable[] consumables)
    {
        for (int i = 0; i < consumables.Length; i++)
        {
            IConsumable consumable = consumables[i];
            if (consumable is IComponent)
            {
                Competitor.Inventory.AddComponent(consumable as IComponent);
            }
            else if(consumable is Bot)
            {
                Competitor.Inventory.AddBot(consumable as Bot);
            }
            else if (consumable is NationalExp)
            {
                NationalExp exp = consumable as NationalExp;
                Experience.AddExp(exp.Nationality, exp.Exp);
            }
            else if (consumable is Currency)
            {
                Wallet.Add(consumable as Currency);
            }
            else
            {
                throw new NotSupportedException(string.Format("This type of consumable [{0}] is not supported by this consumer. ", consumable));
            }
        }
    }

    public IConsumable[] Consume(ILoot[] crateContents, int seed)
    {
        IConsumable[] consumables = new IConsumable[crateContents.Length];
        for (int i = 0; i < crateContents.Length; i++)
        {
            ILoot loot = crateContents[i];
            IConsumable consumable = null;

            if (loot is Currency || loot is IComponent || loot is NationalExp)
            {
                consumable = loot as IConsumable;
            }
            else if (loot is EngineBlueprintData)
            {
                consumable = GetConsumable(loot as EngineBlueprintData, seed);
            }
            else if(loot is WeaponBlueprintData)
            {
                consumable = GetConsumable(loot as WeaponBlueprintData, seed);
            }
            else if(loot is WeaponProcessorBlueprintData)
            {
                consumable = GetConsumable(loot as WeaponProcessorBlueprintData, seed);
            }
            else if(loot is ChassisBlueprintData)
            {
                consumable = GetConsumable(loot as ChassisBlueprintData, seed);
            }
            else if(loot is PlateBlueprintData)
            {
                consumable = GetConsumable(loot as PlateBlueprintData, seed);
            }
            else if(loot is MotherboardBlueprintData)
            {
                consumable = GetConsumable(loot as MotherboardBlueprintData, seed);
            }
            else if(loot is BotBlueprintData)
            {
                consumable = GetConsumable(loot as BotBlueprintData, seed);
            }
            else if(loot is CPUBlueprintData)
            {
                consumable = GetConsumable(loot as CPUBlueprintData, seed);
            }
            else if(loot is DirectiveBlueprintData)
            {
                consumable = GetConsumable(loot as DirectiveBlueprintData, seed);
            }
            else
            {
                throw new NotSupportedException(string.Format("This type of loot [{0}] is not supported by this consumer. ", loot));
            }

            consumables[i] = consumable;
        }

        Consume(consumables);
        return consumables;
    }

    private IConsumable GetConsumable(EngineBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(WeaponBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(WeaponProcessorBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(ChassisBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(PlateBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(MotherboardBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(BotBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(CPUBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
    }
    
    private IConsumable GetConsumable(DirectiveBlueprintData blueprint, int seed)
    {
        return blueprint.DoGenerate(seed);
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
    
    public uint Ascend()
    {
        return Experience.Ascend();
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