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
        }

        private void Update()
        {
            _gameView.Update();
        }
    }
}