using System;
using JunkyardDogs;
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
    [SerializeField] 
    private GameObject[] _levelUpActiveList;
    [SerializeField] 
    private MonoBehaviour[] _levelUpActiveComponentList;
    [SerializeField]
    private Animator[] _ascendAnimators;
    [SerializeField]
    private Button _button;

    [SerializeField, NaughtyAttributes.InfoBox("Optional")] 
    private Image _nationalityFlagImage;
    
    private JunkyardUserViewModel _userViewModel;

    private UserExperienceBreakpoint _breakpoint;

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
        
        foreach (Animator component in _ascendAnimators)
        {
            component.gameObject.SetActive(false);
        }

        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
;        _userViewModel.OnAscend += Ascend;
    }

    private void OnDestroy()
    {
        if (_userViewModel != null)
        {
            _userViewModel.OnAscend -= Ascend;
            _userViewModel = null;
        }
    }

    private void Ascend(Nationality nationality)
    {
        if (_breakpoint is UserNationalExperienceBreakpoint)
        {
            if ((_breakpoint as UserNationalExperienceBreakpoint).Nationality == nationality)
            {
                PlayAscendAnimations();
            }
        }
        else if(nationality == null)
        {
            PlayAscendAnimations();
        }
    }

    private void PlayAscendAnimations()
    {
        foreach (Animator component in _ascendAnimators)
        {
            component.gameObject.SetActive(true);
            component.Play("LevelEffect");
        }
    }

    public void Render(UserExperienceBreakpoint breakpoints)
    {
        _breakpoint = breakpoints;
        _progressDisplay.SetProgress(breakpoints.BreakpointProgress / 100);
        _levelDisplay.text = breakpoints.ExpLevel.Level.ToString();

        foreach (GameObject levelUpGameObject in _levelUpActiveList)
        {
            levelUpGameObject.SetActive(breakpoints.ReadyToUpgrade);
        }
        
        foreach (MonoBehaviour component in _levelUpActiveComponentList)
        {
            component.enabled = breakpoints.ReadyToUpgrade;
        }
    }

    private void OnClick()
    {
        if (_breakpoint != null && _breakpoint.ReadyToUpgrade)
        {
            if (_breakpoint is UserNationalExperienceBreakpoint)
            {
                Game.Instance.GetViewModel<WorldMapViewModel>(0).TryAscend((_breakpoint as UserNationalExperienceBreakpoint).Nationality);
            }
            else
            {
                Game.Instance.GetViewModel<WorldMapViewModel>(0).TryAscend(null);
            }
        }
    }
    
    public void Render(UserNationalExperienceBreakpoint breakpoints)
    {
        Render(breakpoints as UserExperienceBreakpoint);

        if (_nationalityFlagImage != null)
        {
            _nationalityFlagImage.sprite =
                Game.Instance.GetStaticDataPovider<SynchronousStaticDataProvider>()
                    .GetData(SynchronousStaticDataProvider.NationalityImageTypes.Flag, breakpoints.Nationality);
        }
    }
}
