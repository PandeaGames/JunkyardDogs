using UnityEngine;

public class UserExperienceBreakpointsBehaviour : MonoBehaviour
{
    [SerializeField]
    private UserExperienceBreakpointBehaviour _totalExperience;
    
    [SerializeField]
    private UserExperienceBreakpointBehaviour _nationalExperience;
    
    public void Render(UserExperienceBreakpoints breakpoints)
    {
        if (breakpoints.NationalExpBreakpoint.Length > 0)
        {
            UserNationalExperienceBreakpoint nationalBreakpoint = breakpoints.NationalExpBreakpoint[0];
            _nationalExperience.Render(nationalBreakpoint);
        }
        
        _totalExperience.Render(breakpoints.TotalExpBreakpoint);
    }
}
