using JunkyardDogs.Data;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "Tournaments/Participants/SingleSourceParticipant")]
public class SingleSourceParticipantData : ParticipantData
{   
    [SerializeField, CompetitorBlueprintStaticDataReference]
    private CompetitorBlueprintStaticDataReference _competitor;

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