﻿using UnityEngine;
using System.Collections.Generic;
using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using PandeaGames.Data.WeakReferences;
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

    [Header("Rewards")]
    [SerializeField]
    private SpecificationCatalogue _rewards;

    [SerializeField] 
    private int _goldReward;

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
    
    public int GoldReward
    {
        get { return _goldReward; }
    }
    
    public SpecificationCatalogue Rewards
    {
        get { return _rewards; }
    }

    private WeakReference _reference;

    public List<ParticipantStaticDataReference> Participants
    {
        get { return _participants; }
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
    }

    public TournamentBalanceObject GetBalance()
    {
        TournamentBalanceObject balance = new TournamentBalanceObject();

        balance.name = name;
        balance.format = _format == null ? string.Empty : _format.ID;
        balance.roundPaceSeconds = _roundPaceSeconds;
        balance.seasonDelaySeconds = _seasonDelaySeconds;
        balance.participants = string.Join(BalanceData.ListDelimiter, _participants);

        return balance;
    }
}
