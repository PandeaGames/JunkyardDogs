using UnityEngine;
using WeakReference = Data.WeakReference;

[CreateAssetMenu(menuName = "Tournaments/Participants/UserAutoParticipant")]
public class UserAutoParticipantData : ParticipantData
{
    [SerializeField]
    private int _botIndex;
    
    public override Participant GetParticipant()
    {
        UserAutoParticipant participant = new UserAutoParticipant();
        participant.BotIndex = _botIndex;
       return participant;
    }
}