using System.Collections.Generic;
using JunkyardDogs.Simulation;
using UnityEngine;

public static class SimulationDebugUtils
{
    private static Dictionary<string, string> _debugSimulationData = new Dictionary<string, string>();
    
    private const string GenerateSimulationDebugDataPropertyKey = "SimulationDebugUtils_GenerateSimulationDebugDataPropertyKey";
    private const string InitiatorToDebugPropertyKey = "SimulationDebugUtils_InitiatorToDebugPropertyKey";

    public static void SetSimulationDebugData(string key, string data)
    {
        _debugSimulationData[key] = data;
    }
   
    public static string GetSimulationDebugData(string key)
    {
        string output = string.Empty;
        _debugSimulationData.TryGetValue(key, out output);
        return output;
    }
    
    public static bool GenerateSimulationDebugData
    {
        get { return PlayerPrefs.GetInt(GenerateSimulationDebugDataPropertyKey, 0) == 1; }
        set { PlayerPrefs.SetInt(GenerateSimulationDebugDataPropertyKey, value ? 1 : 0); }
    }
    
    public static Initiator InitiatorToDebug
    {
        get { return (Initiator) PlayerPrefs.GetInt(InitiatorToDebugPropertyKey, 0) ; }
        set { PlayerPrefs.SetInt(InitiatorToDebugPropertyKey, (int)(value)); }
    }

    public static List<ISimulationTestExporter> GetAllDataExporters()
    {
        List<ISimulationTestExporter> dataExporters = AssemblyUtils.GetInstances<ISimulationTestExporter>();
        List<ISimulationTestGroupExporter> groupExporters = AssemblyUtils.GetInstances<ISimulationTestGroupExporter>();

        foreach (ISimulationTestGroupExporter simulationTestGroupExporter in groupExporters)
        {
            dataExporters.AddRange(simulationTestGroupExporter.GetDataExporters());
        }

        return dataExporters;
    }
}
