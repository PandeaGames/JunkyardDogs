
using System.Collections.Generic;
using System.Text;
using JunkyardDogs.Simulation;

public class BodyStateExporter : ISimulationTestExporter
{
    public StringBuilder GetData(SimulationTestExportData exportData)
    {
        List<SimBot> simBots = SimulationTestExporterUtils.GetBots(exportData.engagement);
            
        StringBuilder sb = new StringBuilder();
        StringBuilder rowBuilder = new StringBuilder();
        SimBot chosenBot = exportData.inspectingBot == Initiator.BLUE ? simBots[0] : simBots[1];

        rowBuilder.Append("");
        rowBuilder.Append("X");
        rowBuilder.Append(",");
        rowBuilder.Append("Y");
        rowBuilder.Append(",");
        rowBuilder.Append("VX");
        rowBuilder.Append(",");
        rowBuilder.Append("VY");
        rowBuilder.Append(",");
        rowBuilder.Append("Rotation");
        sb.AppendLine(rowBuilder.ToString());
        rowBuilder.Clear();
        
        foreach (SimBot.BodyState state in chosenBot.BodyStates)
        {
            rowBuilder.Append(state.Position.x);
            rowBuilder.Append(",");
            rowBuilder.Append(state.Position.y);
            rowBuilder.Append(",");
            rowBuilder.Append(state.Velocity.x);
            rowBuilder.Append(",");
            rowBuilder.Append(state.Velocity.y);
            rowBuilder.Append(",");
            rowBuilder.Append(state.Rotation);
            
            sb.AppendLine(rowBuilder.ToString());
            rowBuilder.Clear();
            
        }

        return sb;
    }

    public string GetDataName()
    {
        return "BodyStateExporter";
    }
}
