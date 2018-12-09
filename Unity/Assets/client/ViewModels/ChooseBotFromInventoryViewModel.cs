using System;
using JunkyardDogs.Components;
using PandeaGames.ViewModels;
using UnityStandardAssets.Utility;

namespace JunkyardDogs
{
    public class ChooseBotFromInventoryViewModel : AbstractViewModel, ChooseBotScreen.IChooseBotModel
    {
        public event Action OnBotChosen;
        public event Action<Bot> OnBotFocus;
        
        public JunkyardUser User;
        public UserParticipant UserParticipant;
        
        public void Focus(Bot bot)
        {
            if (OnBotFocus != null)
            {
                OnBotFocus(bot);
            }
        }
    
        public void Select(Bot bot)
        {
            UserParticipant.Bot = bot;
            
            if(OnBotChosen != null)
                OnBotChosen();
        }
    }
}