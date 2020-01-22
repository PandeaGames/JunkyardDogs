using System;
using System.Collections.Generic;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakpointData", menuName = "GamePlay/BreakpointData", order = 3)]
public class BreakpointData : AbstractStaticData, IStaticDataBalance<BreakpointBalanceObject>
{
    public List<double> breakpoints;
    
    public void ApplyBalance(BreakpointBalanceObject balance)
    {
        breakpoints = new List<double>();

        ImportBreakpoint(breakpoints, balance.break_01);
        ImportBreakpoint(breakpoints, balance.break_02);
        ImportBreakpoint(breakpoints, balance.break_03);
        ImportBreakpoint(breakpoints, balance.break_04);
        ImportBreakpoint(breakpoints, balance.break_05);
        ImportBreakpoint(breakpoints, balance.break_06);
        ImportBreakpoint(breakpoints, balance.break_07);
        ImportBreakpoint(breakpoints, balance.break_08);
        ImportBreakpoint(breakpoints, balance.break_09);
        ImportBreakpoint(breakpoints, balance.break_10);
        ImportBreakpoint(breakpoints, balance.break_11);
        ImportBreakpoint(breakpoints, balance.break_12);
        ImportBreakpoint(breakpoints, balance.break_13);
        ImportBreakpoint(breakpoints, balance.break_14);
        ImportBreakpoint(breakpoints, balance.break_15);
        ImportBreakpoint(breakpoints, balance.break_16);
        ImportBreakpoint(breakpoints, balance.break_17);
        ImportBreakpoint(breakpoints, balance.break_18);
        ImportBreakpoint(breakpoints, balance.break_19);
        ImportBreakpoint(breakpoints, balance.break_20);
        ImportBreakpoint(breakpoints, balance.break_21);
        ImportBreakpoint(breakpoints, balance.break_22);
        ImportBreakpoint(breakpoints, balance.break_23);
        ImportBreakpoint(breakpoints, balance.break_24);
        ImportBreakpoint(breakpoints, balance.break_25);
    }
    
    private void ImportBreakpoint(List<double> list, double breakpoint)
    {
        if (breakpoint > 0)
        {
            list.Add(breakpoint);
        }
    }

    public BreakpointBalanceObject GetBalance()
    {
        throw new System.NotImplementedException();
    }

    public int GetCompletedBreakpointIndex(double breakpoint)
    {
        int i = 0;
        for (i = 0; i < breakpoints.Count; i++)
        {
            if(breakpoints[i] > breakpoint)
            {
                return i;
            }
        }

        return i;
    }
    
    public int GetCurrentBreakpointIndex(double breakpoint)
    {
        return GetCompletedBreakpointIndex(breakpoint) + 1;
    }
    
    public double GetBreakpointByIndex(int index)
    {
        return breakpoints.Count > 0 ? breakpoints[Math.Min(breakpoints.Count - 1, index)] : 0;
    }
}
