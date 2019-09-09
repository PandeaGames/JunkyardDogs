using System;
using System.Collections.Generic;
using System.Text;
using JunkyardDogs.Simulation;

public class DecisionsAndPriorityExport : ISimulationTestExporter
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

    private int DecisionSorter(SimBot.WeightedDecision a, SimBot.WeightedDecision b)
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

        List<SimBot.WeightedDecision> reorderedDecisions =
            new List<SimBot.WeightedDecision>(chosenBot.WeightedDecisions[0].ToArray());
        reorderedDecisions.Sort(DecisionSorter);

        foreach (SimBot.WeightedDecision weightedDecision in reorderedDecisions)
        {
            rowBuilder.Append(weightedDecision.DecisionMaker.GetType().Name);
            rowBuilder.Append(",");
        }

        sb.AppendLine(rowBuilder.ToString());
        rowBuilder.Clear();

        foreach (List<SimBot.WeightedDecision> weightedDecisionStep in chosenBot.WeightedDecisions)
        {
            reorderedDecisions =
                new List<SimBot.WeightedDecision>(weightedDecisionStep.ToArray());
            reorderedDecisions.Sort(DecisionSorter);
            
            foreach (SimBot.WeightedDecision weightedDecision in reorderedDecisions)
            {
                rowBuilder.Append(weightedDecision.logic.priority);
                rowBuilder.Append(",");
            }

            sb.AppendLine(rowBuilder.ToString());
            rowBuilder.Clear();
        }
        
        
        return sb;
    }

    public string GetDataName()
    {
        return "DecisionPriorities";
    }
}
