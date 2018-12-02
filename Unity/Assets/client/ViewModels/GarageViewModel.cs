using System;
using PandeaGames.ViewModels;
using System.Collections.Generic;
using JunkyardDogs.Components;
using JunkyardDogs.scripts.Runtime.Dialogs;
using JunkyardDogs.Views;
using PandeaGames.Data.WeakReferences;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    public class GarageViewModel : AbstractViewModel
    {
        public struct GarageData
        {
            public readonly JunkyardUser User;
            
            public GarageData( JunkyardUser User)
            {
                this.User = User;
            }
        }

        public event Action<BotBuilder> OnBuilderAdded;
        public event Action<BotBuilder> OnBuilderSelected;
        public event Action<BotBuilder> OnBuilderFocus;
        public event Action OnBuilderBlur;
        public event Action<BotBuilder> OnBuilderDismantled;
        public event Action OnNewBot;
        public event Action OnEditBehaviour;
        public event Action<ChooseFromInventoryViewModel> OnChooseFromInventory;

        private List<BotBuilder> _builders;
        private GarageData _data;
        private BotBuilder _selectedBuilder;
        private BotBuilder _focusedBuilder;

        public List<BotBuilder> Builders
        {
            get { return _builders; }
        }

        public BotBuilder SelectedBuilder
        {
            get { return _selectedBuilder; }
        }
        
        public BotBuilder FocusedBuilder
        {
            get { return _focusedBuilder; }
        }

        public GarageData Data
        {
            get { return _data; }
        }

        public void SetData(GarageData data)
        {
            _data = data;
            _builders = new List<BotBuilder>();
            
            foreach (Bot bot in data.User.Competitor.Inventory.Bots)
            {
                AddBuilder(bot);
            }
        }

        public void SelectBuilder(BotBuilder builder)
        {
            if (_selectedBuilder == builder)
                return;
            
            _selectedBuilder = builder;
            if (OnBuilderSelected != null)
            {
                OnBuilderSelected(builder);
            }
        }
        
        public void FocusSelectedBuilder()
        {
            _focusedBuilder = _selectedBuilder;

            if (OnBuilderFocus != null)
            {
                OnBuilderFocus(_focusedBuilder);
            }
        }
        
        public void BlurBotBuilder()
        {
            _focusedBuilder = null;

            if (OnBuilderBlur != null)
            {
                OnBuilderBlur();
            }
        }
        
        public void DismantleSelected()
        {
            if (_selectedBuilder != null)
            {
                BotBuilder builderToRemove = _selectedBuilder;
                builderToRemove.Dismantle();
                _builders.Remove(builderToRemove);
                
                if (_builders.Count > 0)
                {
                    SelectBuilder(_builders[0]);
                }

                if (OnBuilderDismantled != null)
                {
                    OnBuilderDismantled(builderToRemove);
                }
            }
        }
        
        public void AddBuilder(Chassis chassis)
        {
            AddBuilder(BotBuilder.CreateNewBot(_data.User.Competitor.Inventory, chassis));
        }
        
        public void AddBuilder(Bot bot)
        {
            BotBuilder builder = new BotBuilder(bot, _data.User.Competitor.Inventory);
            AddBuilder(builder);
        }
        
        public void AddBuilder(BotBuilder builder)
        {
            _builders.Add(builder);
            
            if (OnBuilderAdded != null)
            {
                OnBuilderAdded(builder);
            }
        }
        
        public void OnNewBotClicked()
        {
            if (OnNewBot != null)
            {
                OnNewBot();
            }
        }
        
        public void OnEditBehaviourClicked()
        {
            if (OnEditBehaviour != null)
            {
                OnEditBehaviour();
            }
        }

        public void ChooseFromInventory(ChooseFromInventoryViewModel vm)
        {
            if (OnChooseFromInventory != null)
                OnChooseFromInventory(vm);
        }
    }
}