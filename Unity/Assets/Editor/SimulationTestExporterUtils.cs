using System.Collections.Generic;
using JunkyardDogs.Simulation;

public static class SimulationTestExporterUtils
{
    public static List<SimBot> GetBots(SimulatedEngagement engagement)
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
}
