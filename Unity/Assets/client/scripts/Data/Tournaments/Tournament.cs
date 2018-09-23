using UnityEngine;

[CreateAssetMenu(menuName = "Tournaments/Tournament")]
public class Tournament : ScriptableObject
{
    [SerializeField]
    private TournamentFormat _format;

    public TournamentState GenerateState()
    {
        return _format.GenerateState();
    }
}
