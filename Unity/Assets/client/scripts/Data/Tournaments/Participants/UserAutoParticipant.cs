using System;
using WeakReference = Data.WeakReference;
using JunkyardDogs.Components;

public class UserAutoParticipant : Participant
{
    public int BotIndex { get; set; }
    
    public override void GetTeam(JunkyardUser user, Action<ParticipantTeam> onComplete, Action onError)
    {
        TaskProvider.Instance.DelayedAction(() =>
        {
            ParticipantTeam team = new ParticipantTeam(user.Competitor, user.Competitor.Inventory.Bots[BotIndex]);
            onComplete(team);
        });
    }
}