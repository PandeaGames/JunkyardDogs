using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "TestData/TournamentTestData")]
public class TournamentTestData : ScriptableObject
{
    [SerializeField, WeakReference(typeof(Tournament))]
    private WeakReference _tournament;
    
    [SerializeField, WeakReference(typeof(JunkyardDogs.Simulation.Knowledge.State))]
    private WeakReference _state;
    
    [SerializeField, WeakReference(typeof(ParticipantData))]
    private List<WeakReference> _participants;

    public WeakReference Tournament
    {
        get { return _tournament; }
    }
    
    public WeakReference State
    {
        get { return _state; }
    }
    
    public List<WeakReference> Participants
    {
        get { return _participants; }
    }
}
