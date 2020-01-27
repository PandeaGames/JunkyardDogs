using System.Collections.Generic;
using System.Text;
using JunkyardDogs.Simulation;

public struct SimulationTestExportData
{
    public readonly SimulatedEngagement engagement;
    public readonly Initiator inspectingBot;

    public SimulationTestExportData(SimulatedEngagement engagement, Initiator inspectingBot)
    {
        this.engagement = engagement;
        this.inspectingBot = inspectingBot;
    }
}

public interface ISimulationTestExporter
{
    StringBuilder GetData(SimulationTestExportData exportData);
    string GetDataName();
}

public interface ISimulationTestGroupExporter
{
    List<ISimulationTestExporter> GetDataExporters();
}

