using UnityEngine;

namespace JunkyardDogs
{   
    public class GarageTest : MonoBehaviour
    {
        private GarageTestViewController _gameView;
        private void Start()
        {
            _gameView = new GarageTestViewController();
            _gameView.ShowView();
        }

        private void Update()
        {
            _gameView.Update();
        }
    }
}