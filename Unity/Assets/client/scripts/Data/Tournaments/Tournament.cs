using UnityEngine;
using System.Collections.Generic;
using Data;
using PandeaGames.Data.WeakReferences;
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

    private string _guid;
    public string Guid
    {
        get { return _guid; }
    }

    public List<WeakReference> Participants
    {
        get { return _participants; }
    }

    public void SetReferences(string path, string guid)
    {
        _guid = guid;
    }
    
    public TournamentState GenerateState()
    {
        #if UNITY_EDITOR
        _guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));
        #endif
        
        return _format.GenerateState(_guid);
    }
}
