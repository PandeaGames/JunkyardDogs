﻿using UnityEngine;
using WeakReference = Data.WeakReference;

[CreateAssetMenu(menuName = "Tournaments/Participants/SingleSourceParticipant")]
public class SingleSourceParticipantData : ParticipantData
{   
    [SerializeField, WeakReference(typeof(CompetitorBlueprintData))]
    private WeakReference _competitor;

    [SerializeField]
    private int _botIndex;

    public override Participant GetParticipant()
    {
        SingleSourceParticipant participant = new SingleSourceParticipant();
        participant.Source = _competitor;
        participant.BotIndex = _botIndex;
        return participant;
    }
}