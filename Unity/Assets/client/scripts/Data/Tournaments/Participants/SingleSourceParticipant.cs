﻿using System;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class SingleSourceParticipant : Participant
{
    public WeakReference Source { get; set; }
    public int BotIndex { get; set; }
    
    public override void GetTeam(JunkyardUser user, Action<ParticipantTeam> onComplete, Action onError)
    {
        Source.LoadAssetAsync<CompetitorBlueprintData>((data, refernce) =>
        {
            data.GetBlueprint().GenerateObject((generatedObject) =>
            {
                
                Competitor competitor = generatedObject as Competitor;
                ParticipantTeam team = new ParticipantTeam(competitor, competitor.Inventory.Bots[BotIndex]);

                onComplete(team);

            }, onError);
        }, (e) => onError());
    }
}
