
using UnityEngine;
using UnityEngine.UI;

public class MatchStateRenderer : MonoBehaviour
{
    [SerializeField]
    private Color _completeColor;
    
    [SerializeField]
    private Color _waitingColor;
    
    [SerializeField]
    private Color _activeMatchColor;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private RectTransform _userIcon;
    
    public void Render(MatchState state, bool isActiveMatch)
    {
        Color chosenColor = default(Color);
        
        if (state.HasResult())
        {
            chosenColor = _completeColor;
        }
        else if (isActiveMatch)
        {
            chosenColor = _activeMatchColor;
        }
        else
        {
            chosenColor = _waitingColor;
        }

        _image.color = chosenColor;

        bool isUserIcon = state.ParticipantA.Participant is UserParticipant ||
                          state.ParticipantB.Participant is UserParticipant;
        
        _userIcon.gameObject.SetActive(isUserIcon);
    }
}
