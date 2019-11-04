using UnityEngine;
using System.Collections.Generic;
using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using PandeaGames.Data.WeakReferences;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TournamentUnlockCriteria
{
    ExpBreakpoint,
    NationalExpBreakpoint
}

[CreateAssetMenu(menuName = "Tournaments/Tournament")]
public class Tournament : ScriptableObject, IStaticDataBalance<TournamentBalanceObject>
{
    [Header("Format")]
    [SerializeField, TournamentFormatStaticDataReference]
    private TournamentFormatStaticDataReference _format;

    [SerializeField]
    private NationalityStaticDataReference nation;
    
    [SerializeField, ParticipantStaticDataReference]
    private List<ParticipantStaticDataReference> _participants;

    [SerializeField]
    private int _roundPaceSeconds;
    
    [SerializeField]
    private int _seasonDelaySeconds;
    
    [SerializeField]
    private TournamentUnlockCriteria unlockCriteria;
    [SerializeField]
    private NationalityStaticDataReference unlockNation;
    [SerializeField]
    private int nationalExpUnlockBreakpoint;
    [SerializeField]
    private int expUnlockBreakpoint;
    
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

    public void ApplyBalance(TournamentBalanceObject balance)
    {
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
        nationalExpUnlockBreakpoint = balance.expUnlockBreakpoint;
        expUnlockBreakpoint = balance.expUnlockBreakpoint;
         
        _lootCrateRewards = new LootCrateStaticDataReference();
        _lootCrateRewards.ID = balance.lootCrateRewards;
        
        List<ParticipantStaticDataReference> list = new List<ParticipantStaticDataReference>();

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
