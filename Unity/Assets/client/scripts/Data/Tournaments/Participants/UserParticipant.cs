using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using JunkyardDogs.Components;

public class UserParticipant : Participant
{
    public Bot Bot { get; set; }
    
    public override ParticipantTeam GetTeam(JunkyardUser user)
    {
        ParticipantTeam team = new ParticipantTeam(user.Competitor, Bot);
        return team;
    }
}