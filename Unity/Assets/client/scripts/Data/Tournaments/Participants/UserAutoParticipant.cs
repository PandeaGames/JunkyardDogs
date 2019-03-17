using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using JunkyardDogs.Components;

public class UserAutoParticipant : Participant
{
    public int BotIndex { get; set; }
    
    public override ParticipantTeam GetTeam(JunkyardUser user)
    {
        ParticipantTeam team = new ParticipantTeam(user.Competitor, user.Competitor.Inventory.Bots[BotIndex]);
        return team;
    }
}