using UnityEngine;
using System.Collections.Generic;
using Data;
using PandeaGames.Data.WeakReferences;
using UnityEngine.XR.WSA.Input;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Tournaments/Tournament")]
public class Tournament : ScriptableObject, IWeakReferenceObject
{
    [SerializeField]
    private TournamentFormat _format;
    
    [SerializeField, WeakReference(typeof(ParticipantData))]
    private List<WeakReference> _participants;

    [SerializeField] 
    private SpecificationCatalogue _rewards;

    [SerializeField] 
    private int _goldReward;

    private string _guid;
    public string Guid
    {
        get { return _guid; }
    }
    
    public int GoldReward
    {
        get { return _goldReward; }
    }
    
    public SpecificationCatalogue Rewards
    {
        get { return _rewards; }
    }

    private WeakReference _reference;

    public List<WeakReference> Participants
    {
        get { return _participants; }
    }

    public void SetReferences(string path, string guid)
    {
        _guid = guid;
        _reference = new WeakReference();
        _reference.GUID = guid;
        _reference.Path = path;
    }
    
    public TournamentState GenerateState()
    {
        #if UNITY_EDITOR
        _guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));
        #endif
        TournamentState state = _format.GenerateState(_guid);
        state.TournamentReference = _reference;

        return state;
    }
}
