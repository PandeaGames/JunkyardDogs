using JunkyardDogs.Data;
using PandeaGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserExperienceBreakpointBehaviour : MonoBehaviour
{
    [SerializeField]
    private AbstractProgressDisplay _progressDisplay;
    [SerializeField]
    private TMP_Text _levelDisplay;

    [SerializeField, NaughtyAttributes.InfoBox("Optional")] 
    private Image _nationalityFlagImage;

    public void Render(UserExperienceBreakpoint breakpoints)
    {
        _progressDisplay.SetProgress(breakpoints.BreakpointProgress / 100);
        _levelDisplay.text = breakpoints.ExpLevel.Level.ToString();
    }
    
    public void Render(UserNationalExperienceBreakpoint breakpoints)
    {
        Render(breakpoints as UserExperienceBreakpoint);

        if (_nationalityFlagImage != null)
        {
            _nationalityFlagImage.sprite = Game.Instance.GetStaticDataPovider<SynchronousStaticDataProvider>().GetData(SynchronousStaticDataProvider.NationalityImageTypes.Flag, breakpoints.Nationality);
        }
    }
}
