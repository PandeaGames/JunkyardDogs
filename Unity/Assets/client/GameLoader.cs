using Data;
using UnityEngine;

namespace JunkyardDogs
{
    public class GameLoader : MonoBehaviour
    {
        private JunkyardFullGameViewController _gameView;
        private void Start()
        {
            _gameView = new JunkyardFullGameViewController();
            _gameView.ShowView();
        }

        private bool shouldUpdate;

        private void Update()
        {
            if (shouldUpdate)
            {
                _gameView.Update();
                
            }
            shouldUpdate = !shouldUpdate;
        }
    }
}