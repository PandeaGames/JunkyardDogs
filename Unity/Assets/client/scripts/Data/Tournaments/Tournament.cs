using System;
using UnityEngine;
using System.Collections.Generic;
using Data;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine.Serialization;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct TournamentBalanceObject:IStaticDataBalanceObject
{
    public string name;
    public uint grade;
    public string format;
    public string nation;
    public int roundPaceSeconds;
    public int seasonDelaySeconds;
    public string unlockNation;
    public string unlockCriteria;
    public int nationalExpUnlockBreakpoint;
    public int expUnlockBreakpoint;
    public string lootCrateRewards;

    public string stage_0_completions_0;
    public string stage_1_completions_0;
    public string stage_2_completions_0;
    public string stage_0_completions_1;
    public string stage_1_completions_1;
    public string stage_2_completions_1;
    public string stage_0_completions_2;
    public string stage_1_completions_2;
    public string stage_2_completions_2;
    public string stage_0_completions_3;
    public string stage_1_completions_3;
    public string stage_2_completions_3;
    public string stage_0_completions_4;
    public string stage_1_completions_4;
    public string stage_2_completions_4;
    public string stage_0_completions_5;
    public string stage_1_completions_5;
    public string stage_2_completions_5;

    public string participant01;
    public string participant02;
    public string participant03;
    public string participant04;
    public string participant05;
    public string participant06;
    public string participant07;
    public string participant08;
    public string participant09;
    public string participant10;
    public string participant11;
    public string participant12;
    public string participant13;
    public string participant14;
    public string participant15;
    public string participant16;
    public string participant17;
    public string participant18;
    public string participant19;
    public string participant20;
    public string participant21;
    public string participant22;
    public string participant23;
    public string participant24;
    public string participant25;
    public string participant26;
    public string participant27;
    public string participant28;
    public string participant29;
    public string participant30;
    public string participant31;
    
    public string GetDataUID()
    {
        return name;
    }
}

public enum TournamentUnlockCriteria
{
    ExpBreakpoint,
    NationalExpBreakpoint
}

[CreateAssetMenu(menuName = "Tournaments/Tournament")]
public class Tournament : AbstractStaticData, IStaticDataBalance<TournamentBalanceObject>
{
    [Header("Format")]
    [SerializeField, TournamentFormatStaticDataReference]
    private TournamentFormatStaticDataReference _format;

    [SerializeField]
    public NationalityStaticDataReference nation;
    
    [SerializeField, ParticipantStaticDataReference]
    private List<ParticipantStaticDataReference> _participants;

    [SerializeField]
    private int _roundPaceSeconds;
    
    [SerializeField]
    private int _seasonDelaySeconds;

    [SerializeField]
    private ComponentGrade _grade;
    
    [SerializeField]
    private List<TournamentStagesRewards> _rewards;
    public List<TournamentStagesRewards> Rewards {get=>_rewards;}
    
    [SerializeField]
    public TournamentUnlockCriteria unlockCriteria;
    [SerializeField]
    public NationalityStaticDataReference unlockNation;
    [SerializeField]
    public int nationalExpUnlockBreakpoint;

    [SerializeField]
    public int expUnlockBreakpoint;
    
    [SerializeField, LootCrateStaticDataReference]
    private List<LootCrateStaticDataReference> _lootCrateRewardsPerStage;
    
    [SerializeField, LootCrateStaticDataReference]
    private LootCrateStaticDataReference _lootCrateRewards;

    public LootCrateStaticDataReference LootCrateRewards
    {
        get { return _lootCrateRewards; }
    }

    public int RoundPaceSeconds
    {
        get { return _roundPaceSeconds; }
    }
    
    public int SeasonDelaySeconds
    {
        get { return _seasonDelaySeconds; }
    }
    
    private string _guid;
    public string Guid
    {
        get { return name; }
    }

    private WeakReference _reference;

    public List<ParticipantStaticDataReference> Participants
    {
        get { return _participants; }
    }
    
    public List<LootCrateStaticDataReference> LootCrateRewardsPerStage
    {
        get { return _lootCrateRewardsPerStage; }
    }
    
    public TournamentState GenerateState()
    {
        #if UNITY_EDITOR
        _guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));
        #endif
        TournamentState state = _format.Data.GenerateState(Guid);
        state.TournamentReference = new TournamentStaticDataReference();
        state.TournamentReference.ID = name;

        return state;
    }

    [Serializable]
    public struct TournamentStagesRewards
    {
        [LootCrateStaticDataReference]
        public List<LootCrateStaticDataReference> Rewards;
    }
    
    public void ApplyBalance(TournamentBalanceObject balance)
    {
        _grade = new ComponentGrade(balance.grade);
        _format = new TournamentFormatStaticDataReference();
        _participants = new List<ParticipantStaticDataReference>();
        _lootCrateRewardsPerStage = new List<LootCrateStaticDataReference>();
        nation = new NationalityStaticDataReference();
        unlockNation = new NationalityStaticDataReference();

        _roundPaceSeconds = balance.roundPaceSeconds;
        _seasonDelaySeconds = balance.seasonDelaySeconds;

        _format.ID = balance.format;

        nation.ID = balance.nation;
        unlockNation.ID = balance.unlockNation;
        unlockCriteria = BalanceDataUtilites.DecodeEnumSingle<TournamentUnlockCriteria>(balance.unlockCriteria);
        nationalExpUnlockBreakpoint = balance.nationalExpUnlockBreakpoint;
        expUnlockBreakpoint = balance.expUnlockBreakpoint;
         
        _lootCrateRewards = new LootCrateStaticDataReference();
        _lootCrateRewards.ID = balance.lootCrateRewards;
        
        List<ParticipantStaticDataReference> list = new List<ParticipantStaticDataReference>();

        _rewards = new List<TournamentStagesRewards>();
        
        ImportRewards(
            new string[]{balance.stage_0_completions_0, balance.stage_1_completions_0, balance.stage_2_completions_0}, 
            ref _rewards);
        ImportRewards(
            new string[]{balance.stage_0_completions_1, balance.stage_1_completions_1, balance.stage_2_completions_1}, 
            ref _rewards);
        ImportRewards(
            new string[]{balance.stage_0_completions_2, balance.stage_1_completions_2, balance.stage_2_completions_2}, 
            ref _rewards);
        ImportRewards(
            new string[]{balance.stage_0_completions_3, balance.stage_1_completions_3, balance.stage_2_completions_3}, 
            ref _rewards);
        ImportRewards(
            new string[]{balance.stage_0_completions_4, balance.stage_1_completions_0, balance.stage_2_completions_4}, 
            ref _rewards);
        ImportRewards(
            new string[]{balance.stage_0_completions_5, balance.stage_1_completions_5, balance.stage_2_completions_5}, 
            ref _rewards);
        
        ImportParticipant(list, balance.participant01);
        ImportParticipant(list, balance.participant02);
        ImportParticipant(list, balance.participant03);
        ImportParticipant(list, balance.participant04);
        ImportParticipant(list, balance.participant05);
        ImportParticipant(list, balance.participant06);
        ImportParticipant(list, balance.participant07);
        ImportParticipant(list, balance.participant08);
        ImportParticipant(list, balance.participant09);
        ImportParticipant(list, balance.participant10);
        ImportParticipant(list, balance.participant11);
        ImportParticipant(list, balance.participant12);
        ImportParticipant(list, balance.participant13);
        ImportParticipant(list, balance.participant14);
        ImportParticipant(list, balance.participant15);
        ImportParticipant(list, balance.participant16);
        ImportParticipant(list, balance.participant17);
        ImportParticipant(list, balance.participant18);
        ImportParticipant(list, balance.participant19);
        ImportParticipant(list, balance.participant20);
        ImportParticipant(list, balance.participant21);
        ImportParticipant(list, balance.participant22);
        ImportParticipant(list, balance.participant23);
        ImportParticipant(list, balance.participant24);
        ImportParticipant(list, balance.participant25);
        ImportParticipant(list, balance.participant26);
        ImportParticipant(list, balance.participant27);
        ImportParticipant(list, balance.participant28);
        ImportParticipant(list, balance.participant29);
        ImportParticipant(list, balance.participant30);
        ImportParticipant(list, balance.participant31);

        _participants = list;

    }

    private void ImportParticipant(List<ParticipantStaticDataReference> list, string participantId)
    {
        if (!string.IsNullOrEmpty(participantId))
        {
            ParticipantStaticDataReference reference = new ParticipantStaticDataReference();
            reference.ID = participantId;
            list.Add(reference);
        }
    }

    private void ImportRewards(string[] stagedRewards, ref List<TournamentStagesRewards> rewards)
    {
        List<LootCrateStaticDataReference> stagesRewardsList = null;
        foreach (string stagedReward in stagedRewards)
        {
            if (!string.IsNullOrEmpty(stagedReward))
            {
                if (stagesRewardsList == null)
                {
                    stagesRewardsList = new List<LootCrateStaticDataReference>();
                }

                LootCrateStaticDataReference reference = new LootCrateStaticDataReference();
                reference.ID = stagedReward;
                stagesRewardsList.Add(reference);
            }
        }

        if (stagesRewardsList != null)
        {
            rewards.Add(new TournamentStagesRewards(){Rewards = stagesRewardsList});
        }
    }

    public TournamentBalanceObject GetBalance()
    {
        TournamentBalanceObject balance = new TournamentBalanceObject();

        balance.name = name;
        balance.format = _format == null ? string.Empty : _format.ID;
        balance.roundPaceSeconds = _roundPaceSeconds;
        balance.seasonDelaySeconds = _seasonDelaySeconds;
        balance.lootCrateRewards = string.Join(BalanceData.ListDelimiter, _lootCrateRewardsPerStage);

        return balance;
    }
}
