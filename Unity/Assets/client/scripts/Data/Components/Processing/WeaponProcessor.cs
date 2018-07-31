using UnityEngine;
using System.Collections;
using JunkyardDogs.Specifications;
using System;

namespace JunkyardDogs.Components
{

    [Serializable]
    public class WeaponProcessor : PhysicalComponent
    {
        public Components.Weapon Weapon { get; set; }

        public WeaponProcessor()
        {

        }

        public override void LoadAsync(Action onLoadSuccess, Action onLoadFailed)
        {
            int objectsToLoad = 0;
            bool hasError = false;

            Action onInternalLoadSuccess = () =>
            {
                if (--objectsToLoad <= 0)
                {
                    if (hasError)
                    {
                        onLoadFailed();
                    }
                    else
                    {
                        onLoadSuccess();
                    }
                }
            };

            Action onInternalLoadError = () =>
            {
                hasError = true;

                if (--objectsToLoad <= 0)
                {
                    onLoadFailed();
                }
            };

            objectsToLoad++;
            base.LoadAsync(onInternalLoadSuccess, onInternalLoadError);

            if(Weapon != null)
            {
                objectsToLoad++;
                Weapon.LoadAsync(onInternalLoadSuccess, onInternalLoadError);
            }
        }
    }
}