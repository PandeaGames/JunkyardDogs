
    using JunkyardDogs.Simulation;
    using UnityEngine;

    public class SimpleSimulatedBotView : SimpleSimulatedPhysicsObjectView
    {
        private SimBot simBot;
        
        public SimpleSimulatedBotView(SimpleSimulatedEngagement viewContainer, SimBot simObject) : base(viewContainer, simObject)
        {
            this.simBot = simObject as SimBot;
        }

        protected override GameObject GenerateView()
        {
            GameObject botObject =
                GameObject.Instantiate(viewContainer.botPrefabFactory.GetAsset(simBot.bot.Chassis.Specification)) as GameObject;
                
            BotRenderer renderer = botObject.AddComponent<BotRenderer>();
                
            renderer.Render(simBot.bot, viewContainer.botRenderConfiguration);
            return renderer.gameObject;
        }
    }
