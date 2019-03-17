
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
    
    public static void GenerateParticipantsAsync(List<WeakReference> participantReferences, Action<List<Participant>> onComplete, Action onError)
    {
        List<ParticipantData> participants = new List<ParticipantData>();
        
        participantReferences.ForEach((participantReference) => participantReference.LoadAssetAsync<ParticipantData>(
            (participantData, reference) =>
            {
                participants.Add(participantData);

                if (participantReferences.Count == participants.Count)
                {
                    onComplete(GenerateParticipants(participants));
                }
            }, (e) => onError()));
    }
}

public abstract class ParticipantData : ScriptableObject
{
    public abstract Participant GetParticipant();
}