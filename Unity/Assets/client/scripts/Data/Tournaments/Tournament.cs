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

[CreateAssetMenu(menuName = "Tournaments/Tournament")]
public class Tournament : ScriptableObject, IStaticDataBalance<TournamentBalanceObject>
{
    [Header("Format")]
    [SerializeField, TournamentFormatStaticDataReference]
    private TournamentFormatStaticDataReference _format;
    
    [SerializeField, ParticipantStaticDataReference]
    private List<ParticipantStaticDataReference> _participants;

    [SerializeField]
    private int _roundPaceSeconds;
    
    [SerializeField]
    private int _seasonDelaySeconds;

    [SerializeField, LootCrateStaticDataReference]
    private List<LootCrateStaticDataReference> _lootCrateRewardsPerStage;

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
        TournamentState state = _format.Data.GenerateState(_guid);
        state.TournamentReference = new TournamentStaticDataReference();
        state.TournamentReference.ID = this.name;

        return state;
    }

    public void ApplyBalance(TournamentBalanceObject balance)
    {
        _format = new TournamentFormatStaticDataReference();
        _participants = new List<ParticipantStaticDataReference>();
        _lootCrateRewardsPerStage = new List<LootCrateStaticDataReference>();

        _roundPaceSeconds = balance.roundPaceSeconds;
        _seasonDelaySeconds = balance.seasonDelaySeconds;

        _format.ID = balance.format;

        string[] participants = balance.participants.Split(BalanceData.ListDelimiterChar);

        foreach (string participantId in participants)
        {
            ParticipantStaticDataReference reference = new ParticipantStaticDataReference();
            reference.ID = participantId;
            _participants.Add(reference);
        }
        
        string[] lootCrateRewards = balance.lootCrateRewards.Split(BalanceData.ListDelimiterChar);

        foreach (string lootCrateId in lootCrateRewards)
        {
            LootCrateStaticDataReference reference = new LootCrateStaticDataReference();
            reference.ID = lootCrateId;
            _lootCrateRewardsPerStage.Add(reference);
        }
    }

    public TournamentBalanceObject GetBalance()
    {
        TournamentBalanceObject balance = new TournamentBalanceObject();

        balance.name = name;
        balance.format = _format == null ? string.Empty : _format.ID;
        balance.roundPaceSeconds = _roundPaceSeconds;
        balance.seasonDelaySeconds = _seasonDelaySeconds;
        balance.participants = string.Join(BalanceData.ListDelimiter, _participants);
        balance.lootCrateRewards = string.Join(BalanceData.ListDelimiter, _lootCrateRewardsPerStage);

        return balance;
    }
}
