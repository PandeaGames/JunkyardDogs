using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using JunkyardDogs.Components;

public class UserParticipant : Participant
{
    public Bot Bot { get; set; }
    
    public override void GetTeam(JunkyardUser user, Action<ParticipantTeam> onComplete, Action onError)
    {
        TaskProvider.Instance.DelayedAction(() =>
        {
            ParticipantTeam team = new ParticipantTeam(user.Competitor, Bot);
            onComplete(team);
        });
    }
}