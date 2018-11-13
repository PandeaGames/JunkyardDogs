using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PandeaGames.Views.Screens
{
    public class NestedWindowView : WindowView
    {
        private List<ConfiguredScreen> _stack;

        protected override void Start()
        {
            base.Start();
            _stack = new List<ConfiguredScreen>();
        }

        public override void LaunchScreen(string sceneId)
        {
            _stack.Add(new ConfiguredScreen(sceneId));
            base.LaunchScreen(sceneId);
        }

        public override void Back()
        {
            //there is no stack, so no navigation can happen
            if (_stack.Count == 0)
                return;

            //we are currently viewing the last screen. Clear the window completely.
            if (_stack.Count == 1)
            {
                Close();
                _stack.Clear();
                return;
            }

            _stack.RemoveAt(_stack.Count - 1);
            ConfiguredScreen screen = _stack[_stack.Count - 1];

            LaunchScreen(new ScreenTransition(screen.SceneId, Direction.FROM));
        }
    }
}
