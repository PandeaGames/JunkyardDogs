using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "Tournaments/Participants/SingleSourceParticipant")]
public class SingleSourceParticipantData : ParticipantData, IStaticDataBalance<SingleSourceParticipantBalanceObject>
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

    public void ApplyBalance(SingleSourceParticipantBalanceObject balance)
    {
        this.name = balance.name;
        _competitor = new CompetitorBlueprintStaticDataReference();
        _competitor.ID = balance.competitor;
        _botIndex = balance.botIndex;
    }

    public SingleSourceParticipantBalanceObject GetBalance()
    {
        throw new System.NotImplementedException();
    }
}