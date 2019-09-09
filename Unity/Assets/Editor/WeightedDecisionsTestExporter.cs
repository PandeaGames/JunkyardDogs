using System;
using System.Collections.Generic;
using System.Text;
using JunkyardDogs.Simulation;


    public class WeightedDecisionsTestExporter : ISimulationTestExporter
    {
        public StringBuilder GetData(SimulationTestExportData exportData)
        {
            List<SimBot> simBots = SimulationTestExporterUtils.GetBots(exportData.engagement);
            
            StringBuilder sb = new StringBuilder();
            StringBuilder rowBuilder = new StringBuilder();

            int numberOfDecisions = Math.Max(simBots[0].WeightedDecisions.Count, simBots[1].WeightedDecisions.Count);

            for (int j = 0; j < simBots.Count; j++)
            {
                rowBuilder.Append(string.Join(",", new string[]
                {
                    "Bot "+ j + " Weight",
                    "Bot "+ j + " Decision"
                }));
            
                rowBuilder.Append(",");
            }

            sb.AppendLine(rowBuilder.ToString());
            rowBuilder.Clear();

            for (var i = 0; i < numberOfDecisions; i++)
            {
                for (int j = 0; j < simBots.Count; j++)
                {
                    if (j != 0)
                    {
                        rowBuilder.Append(",");
                    }
                    SimBot simBot = simBots[j];
                    if (simBot.WeightedDecisions.Count > i)
                    {
                        SimBot.WeightedDecision weightedDecisions = simBot.WeightedDecisions[i][0];
                        rowBuilder.Append(string.Join(",", new string[]
                        {
                            weightedDecisions.Priority.ToString(),
                            weightedDecisions.DecisionMaker.GetType().Name 
                        }));
                    }
                    else
                    {
                        rowBuilder.Append(",");
                    }
                }

                sb.AppendLine(rowBuilder.ToString());
                rowBuilder.Clear();
            }

            return sb;
        }
        
        public string GetDataName()
        {
            return "Top Decisions";
        }
    }
