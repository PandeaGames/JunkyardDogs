using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using JunkyardDogs.Data.Balance;

[CreateAssetMenu(fileName = "Nationality", menuName = "GamePlay/Nationality", order = 3)]
public class Nationality : AbstractStaticData, IStaticDataBalance<NationBalanceObject>
{
    [SerializeField]
    public Distinction[] Distinctions;

    [SerializeField, BreakpointStaticDataReference]
    public BreakpointStaticDataReference breakpoints;
    
    public void ApplyBalance(NationBalanceObject balance)
    {
        this.name = balance.name;
        breakpoints = new BreakpointStaticDataReference();
        breakpoints.ID = balance.breakpoints;
        
        List<Distinction> list = new List<Distinction>();

        ProcessDistinction(list, balance.distinctionId_01, balance.distinctionValue_01);
        ProcessDistinction(list, balance.distinctionId_02, balance.distinctionValue_02);
        ProcessDistinction(list, balance.distinctionId_03, balance.distinctionValue_03);
        ProcessDistinction(list, balance.distinctionId_04, balance.distinctionValue_04);
        ProcessDistinction(list, balance.distinctionId_05, balance.distinctionValue_05);

        Distinctions = list.ToArray();
    }

    private void ProcessDistinction(List<Distinction> list, string name, int value)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Distinction Distinction = new Distinction();
            Distinction.Type = (DistinctionType) Enum.Parse(typeof (DistinctionType), name);
            Distinction.Value = value;
            list.Add(Distinction);
        }
    }

    public NationBalanceObject GetBalance()
    {
        NationBalanceObject balance = new NationBalanceObject();
        balance.name = this.name;
        //balance.distinctions = BalanceDataUtilites.(balance.distinctions);
        return balance;
    }
}