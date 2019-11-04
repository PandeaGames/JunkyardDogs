using System.Collections.Generic;
using JunkyardDogs.Data.Balance;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakpointData", menuName = "GamePlay/BreakpointData", order = 3)]
public class BreakpointData : ScriptableObject, IStaticDataBalance<BreakpointBalanceObject>
{
    public List<int> breakpoints;
    
    public void ApplyBalance(BreakpointBalanceObject balance)
    {
        breakpoints = new List<int>();

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
    
    private void ImportBreakpoint(List<int> list, int breakpoint)
    {
        Debug.Log("ImportBreakpoint");
        if (breakpoint > 0)
        {
            Debug.Log("Import " +breakpoint.ToString());
            list.Add(breakpoint);
        }
    }

    public BreakpointBalanceObject GetBalance()
    {
        throw new System.NotImplementedException();
    }
}
