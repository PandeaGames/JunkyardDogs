using UnityEngine;
using WeakReference = Data.WeakReference;

[CreateAssetMenu(menuName = "Tournaments/Participants/UserParticipant")]
public class UserParticipantData : ParticipantData
{   
    public override Participant GetParticipant()
    {
       UserParticipant participant = new UserParticipant();
       return participant;
    }
}