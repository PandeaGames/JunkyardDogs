﻿using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;
using Data;

namespace JunkyardDogs.Components
{

    [Serializable]
    public class WeaponProcessor : PhysicalComponent
    {
        public Components.Weapon Weapon { get; set; }

        public WeaponProcessor()
        {

        }

        public override void Dismantle(Inventory inventory)
        {
            base.Dismantle(inventory);

            if (Weapon != null)
            {
                Weapon.Dismantle(inventory);
                Weapon = null;
            }
        }

        public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
        {
            Loader loader = new Loader();

            loader.AppendProvider(Weapon);
            loader.AppendProvider(base.LoadAsync);
            
            loader.LoadAsync(onLoadSuccess, onLoadFailed);
        }
    }
}