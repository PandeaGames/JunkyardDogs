using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JunkyardDogs.Components
{
    public class WeaponProcessorBuilder
    {
        private WeaponProcessor _processor;
        private Inventory _inventory;

        public WeaponProcessorBuilder(Inventory inventory, WeaponProcessor processor)
        {
            _processor = processor;
            _inventory = inventory;
        }

        public void SetWeapon(Weapon weapon)
        {
            if(_processor.Weapon != null)
            {
                _inventory.AddComponent(_processor.Weapon);
            }

            _inventory.RemoveComponent(weapon);
            _processor.Weapon = weapon;
        }
    }

    public class BotBuilder
    {
        private Bot _bot;
        private Inventory _inventory;
        private Dictionary<Chassis.ArmamentLocation, WeaponProcessorBuilder> _processorBuilders;

        public Bot Bot { get { return _bot; } }
        public Inventory Inventory { get { return _inventory; } }
            
        public static BotBuilder CreateNewBot(Inventory inventory, Chassis chassis)
        {
            Bot bot = new Bot(chassis);
            BotBuilder builder = new BotBuilder(bot, inventory);
            inventory.RemoveComponent(chassis);
            inventory.AddBot(bot);
            return builder;
        }

        public BotBuilder(Bot bot, Inventory inventory)
        {
            _bot = bot;
            _inventory = inventory;
            _processorBuilders = new Dictionary<Chassis.ArmamentLocation, WeaponProcessorBuilder>();

            SetupProgessorBuilder(bot, inventory, Chassis.ArmamentLocation.Front);
            SetupProgessorBuilder(bot, inventory, Chassis.ArmamentLocation.Left);
            SetupProgessorBuilder(bot, inventory, Chassis.ArmamentLocation.Right);
            SetupProgessorBuilder(bot, inventory, Chassis.ArmamentLocation.Top);
        }

        public void Dismantle()
        {
            _inventory.DismantleBot(_bot);
        }

        private void SetupProgessorBuilder(Bot bot, Inventory inventory, Chassis.ArmamentLocation location)
        {
            WeaponProcessor processor = bot.Chassis.GetWeaponProcessor(location);

            if (processor != null)
            {
                _processorBuilders.Add(location, new WeaponProcessorBuilder(inventory, processor));
            }
        }

        public void RemovePlate(Chassis.PlateLocation location, int index)
        {
            Plate plate =_bot.Chassis.RemovePlate(location, index);

            if(plate != null)
            {
                _inventory.RemoveComponent(plate);
            }
        }

        public void SetPlate(Plate plate, Chassis.PlateLocation location, int index)
        {
            _inventory.RemoveComponent(plate);
            Plate removedPlate = _bot.Chassis.SetPlate(plate, location, index);
            
            if(removedPlate != null)
            {
                _inventory.AddComponent(removedPlate);
            }
        }

        public void RemoveWeaponProcessor(Chassis.ArmamentLocation location)
        {
            WeaponProcessor processor = null;

            switch (location)
            {
                case Chassis.ArmamentLocation.Front:
                    processor = _bot.Chassis.FrontArmament;
                    _bot.Chassis.FrontArmament = null;
                    break;
                case Chassis.ArmamentLocation.Left:
                    processor = _bot.Chassis.LeftArmament;
                    _bot.Chassis.LeftArmament = null;
                    break;
                case Chassis.ArmamentLocation.Right:
                    processor = _bot.Chassis.RightArmament;
                    _bot.Chassis.RightArmament = null;
                    break;
                case Chassis.ArmamentLocation.Top:
                    processor = _bot.Chassis.TopArmament;
                    _bot.Chassis.TopArmament = null;
                    break;
            }

            if(processor != null)
            {
                if(processor.Weapon != null)
                {
                    _inventory.AddComponent(processor.Weapon);
                }

                processor.Weapon = null;
                _inventory.AddComponent(processor);
            }
        }

        public WeaponProcessorBuilder GetWeaponProcessorBuilder(Chassis.ArmamentLocation location)
        {
            WeaponProcessorBuilder builder = null;
            _processorBuilders.TryGetValue(location, out builder);
            return builder;
        }

        public void SetWeaponProcessor(WeaponProcessor processor, Chassis.ArmamentLocation location)
        {
            if(!_inventory.ContainsComponent(processor))
            {
                return;
            }

            RemoveWeaponProcessor(location);
            _inventory.RemoveComponent(processor);
            _processorBuilders[location] = new WeaponProcessorBuilder(_inventory, processor);

            switch (location)
            {
                case Chassis.ArmamentLocation.Front:
                    _bot.Chassis.FrontArmament = processor;
                    break;
                case Chassis.ArmamentLocation.Left:
                    _bot.Chassis.LeftArmament = processor;
                    break;
                case Chassis.ArmamentLocation.Right:
                    _bot.Chassis.RightArmament = processor;
                    break;
                case Chassis.ArmamentLocation.Top:
                    _bot.Chassis.TopArmament = processor;
                    break;
            }
        }
    }
}
