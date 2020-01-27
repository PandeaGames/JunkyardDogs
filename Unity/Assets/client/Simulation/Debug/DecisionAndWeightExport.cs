using System;
using System.Collections.Generic;
using System.Text;
using JunkyardDogs.Simulation;

public class DecisionsAndWeightExport : ISimulationTestExporter
{
    private List<SimBot> GetBots(SimulatedEngagement engagement)
    {
        List<SimBot> bots = new List<SimBot>();

        foreach (SimObject simObject in engagement.ObjectHistory)
        {
            if (simObject is SimBot)
            {
                bots.Add(simObject as SimBot);
            }
        }

        return bots;
    }

    private int DecisionSorter(SimBotDecisionPlane.WeightedDecision a, SimBotDecisionPlane.WeightedDecision b)
    {
        string aName = a.DecisionMaker.GetType().Name;
        string bName = b.DecisionMaker.GetType().Name;
        return aName.CompareTo(bName);
    }
    
    public StringBuilder GetData(SimulationTestExportData exportData)
    {
        List<SimBot> simBots = GetBots(exportData.engagement);

        SimBot chosenBot = exportData.inspectingBot == Initiator.BLUE ? simBots[0] : simBots[1];
        
        StringBuilder sb = new StringBuilder();
        StringBuilder rowBuilder = new StringBuilder();

        List<SimBotDecisionPlane.WeightedDecision> reorderedDecisions =
            new List<SimBotDecisionPlane.WeightedDecision>(chosenBot.GetDecisionPlane(DecisionPlane.Base).WeightedDecisions[0].ToArray());
        reorderedDecisions.Sort(DecisionSorter);

        foreach (SimBotDecisionPlane.WeightedDecision weightedDecision in reorderedDecisions)
        {
            rowBuilder.Append(weightedDecision.DecisionMaker.GetType().Name);
            rowBuilder.Append(",");
        }

        sb.AppendLine(rowBuilder.ToString());
        rowBuilder.Clear();

        foreach (List<SimBotDecisionPlane.WeightedDecision> weightedDecisionStep in chosenBot.GetDecisionPlane(DecisionPlane.Base).WeightedDecisions)
        {
            reorderedDecisions =
                new List<SimBotDecisionPlane.WeightedDecision>(weightedDecisionStep.ToArray());
            reorderedDecisions.Sort(DecisionSorter);
            
            foreach (SimBotDecisionPlane.WeightedDecision weightedDecision in reorderedDecisions)
            {
                rowBuilder.Append(weightedDecision.logic.weight);
                rowBuilder.Append(",");
            }

            sb.AppendLine(rowBuilder.ToString());
            rowBuilder.Clear();
        }
        
        
        return sb;
    }

    public string GetDataName()
    {
        return "DecisionWeights";
    }
}
