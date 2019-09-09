
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JunkyardDogs.Simulation;

public class DecisionLogicGroupExporter : ISimulationTestGroupExporter
{
    private List<Type> decisionMakerTypes;
    
    public DecisionLogicGroupExporter()
    {
        decisionMakerTypes = new List<Type>();
        List<IDecisionMaker> decisionMakers = AssemblyUtils.GetInstances<IDecisionMaker>();

        foreach (IDecisionMaker decisionMaker in decisionMakers)
        {
            decisionMakerTypes.Add(decisionMaker.GetType());
        }
    }
    
    public List<ISimulationTestExporter> GetDataExporters()
    {
        List<ISimulationTestExporter> exporters = new List<ISimulationTestExporter>();

        foreach (Type type in decisionMakerTypes)
        {
            exporters.Add(new LogicExporter(type));
        }

        return exporters;
    }

    public class LogicExporter : ISimulationTestExporter
    {
        private Type decisionMakerType;
        public LogicExporter(Type logicDecisionMakerType)
        {
            this.decisionMakerType = logicDecisionMakerType;
        }
        
        public string GetDataName()
        {
            return decisionMakerType.Name;
        }
        
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
        
        public StringBuilder GetData(SimulationTestExportData exportData)
        {
            List<SimBot> simBots = GetBots(exportData.engagement);
            StringBuilder sb = new StringBuilder();
            StringBuilder rowBuilder = new StringBuilder();
            
            SimBot chosenBot = exportData.inspectingBot == Initiator.BLUE ? simBots[0] : simBots[1];

            FieldInfo[] fieldInfos = null;

            foreach (List<SimBot.WeightedDecision> weightedDecisionHistory in chosenBot.WeightedDecisions)
            {
                foreach (SimBot.WeightedDecision weightedDecision in weightedDecisionHistory)
                {
                    if (weightedDecision.DecisionMaker.GetType().ToString().Equals(decisionMakerType.ToString()))
                    {
                        fieldInfos =
                            weightedDecision.logic.GetType().GetFieldInfoIncludingParents(
                                BindingFlags.Public | 
                                BindingFlags.Instance | 
                                BindingFlags.NonPublic);
                    }
                }
            }

            
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                rowBuilder.Append(fieldInfo.Name);
                rowBuilder.Append(",");
            }
            
            sb.AppendLine(rowBuilder.ToString());
            rowBuilder.Clear();

            foreach (List<SimBot.WeightedDecision> weightedDecisionHistory in chosenBot.WeightedDecisions)
            {
                foreach (SimBot.WeightedDecision weightedDecision in weightedDecisionHistory)
                {
                    if (weightedDecision.DecisionMaker.GetType().Equals(decisionMakerType))
                    {
                        foreach (FieldInfo fieldInfo in fieldInfos)
                        {
                            rowBuilder.Append(fieldInfo.GetValue(weightedDecision.logic));
                            rowBuilder.Append(",");
                        }
                        
                        break;
                    }
                }
                
                sb.AppendLine(rowBuilder.ToString());
                rowBuilder.Clear();
            }

            return sb;
        }
    }
}
