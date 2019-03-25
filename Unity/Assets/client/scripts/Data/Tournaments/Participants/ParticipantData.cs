
using UnityEngine;
using System.Collections.Generic;
using System;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public static class ParticipantDataUtils
{
    public static List<Participant> GenerateParticipants(List<ParticipantData> participantData)
    {
        List<Participant> participants = new List<Participant>();
        
        participantData.ForEach((data) => { participants.Add(data.GetParticipant()); });

        return participants;
    }
    
    public static List<Participant> GenerateParticipants(List<ParticipantStaticDataReference> participantData)
    {
        List<Participant> participants = new List<Participant>();
        
        participantData.ForEach((data) => { participants.Add(data.Data.GetParticipant()); });

        return participants;
    }
}

public abstract class ParticipantData : ScriptableObject
{
    public abstract Participant GetParticipant();
}