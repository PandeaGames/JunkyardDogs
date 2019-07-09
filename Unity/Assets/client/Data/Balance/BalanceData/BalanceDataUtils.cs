using System;
using System.Collections.Generic;
using JunkyardDogs.Simulation;

namespace JunkyardDogs.Data.Balance
{
    public static class BalanceDataUtils
    {
        public static void ProcessDistinction(List<Distinction> list, string name, int value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Distinction Distinction = new Distinction();
                Distinction.Type = (DistinctionType) Enum.Parse(typeof (DistinctionType), name);
                Distinction.Value = value;
                list.Add(Distinction);
            }
        }
    }
}