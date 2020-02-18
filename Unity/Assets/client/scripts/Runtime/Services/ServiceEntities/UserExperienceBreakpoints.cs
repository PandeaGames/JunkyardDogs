using System;
using JunkyardDogs.Components.Gameplay;
using PandeaGames.Utils;

public class UserExperienceBreakpoint
{
    public readonly ExpLevel ExpLevel;
    public readonly float BreakpointProgress;

    public bool ReadyToUpgrade => BreakpointProgress >= 100;

    public UserExperienceBreakpoint(ExpLevel ExpLevel, float BreakpointProgress)
    {
        this.ExpLevel = ExpLevel;
        this.BreakpointProgress = BreakpointProgress;
    }
}

public class UserNationalExperienceBreakpoint : UserExperienceBreakpoint
{
    public readonly Nationality Nationality;

    public UserNationalExperienceBreakpoint(
        Nationality nationality, 
        ExpLevel expLevel, 
        float breakpointProgress) 
        : base(expLevel, breakpointProgress)
    {
        this.Nationality = nationality;
    }
}

public class UserExperienceBreakpoints
{
    public readonly UserExperienceBreakpoint TotalExpBreakpoint;
    public readonly UserNationalExperienceBreakpoint[] NationalExpBreakpoint;

    public UserExperienceBreakpoints(
        BreakpointData totalExpBreakpointData, 
        BreakpointData nationalExpBreakpointData, 
        Experience exp)
    {
        int currentBreakpointIndex = (int) exp.Level - 1;
        double currentBreakpoint = totalExpBreakpointData.breakpoints[currentBreakpointIndex];
        float progress = ((float) exp.Value / (float)currentBreakpoint) * 100;
        
        TotalExpBreakpoint = new UserExperienceBreakpoint(exp, progress);
        NationalExpBreakpoint = new UserNationalExperienceBreakpoint[exp.NationDictionary.Count];

        for (int i = 0; i < NationalExpBreakpoint.Length; i++)
        {
            NationDictionaryKvP nationalExp = exp.NationDictionary.GetPair(i);
            currentBreakpointIndex = (int) Math.Min(nationalExp.Value.Level - 1, nationalExpBreakpointData.breakpoints.Count);
            currentBreakpoint = nationalExpBreakpointData.breakpoints[currentBreakpointIndex];
            progress = ((float) nationalExp.Value.Value / (float)currentBreakpoint) * 100;

            NationalExpBreakpoint[i] = new UserNationalExperienceBreakpoint(nationalExp.Key, nationalExp.Value, progress);
        }
    }
    
    public UserExperienceBreakpoints(
        UserExperienceBreakpoint TotalExpBreakpoint, 
        UserNationalExperienceBreakpoint[] NationalExpBreakpoint)
    {
        this.TotalExpBreakpoint = TotalExpBreakpoint;
        this.NationalExpBreakpoint = NationalExpBreakpoint;
    }
}
