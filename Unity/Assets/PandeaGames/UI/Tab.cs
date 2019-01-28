using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandeaGames.UI
{
    public class Tab : MonoBehaviour
    {
        public event Action<Tab> OnRequestFocus;
        
        public virtual void Focus()
        {

        }

        public virtual void Blur()
        {

        }

        protected void RequestFocus()
        {
            if (OnRequestFocus != null)
            {
                OnRequestFocus(this);
            }
        }
    }
}