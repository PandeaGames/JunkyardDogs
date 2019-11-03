using UnityEngine;

namespace PandeaGames.Runtime.Dialogs
{
    public class DialogDestructor : MonoBehaviour
    {
        [SerializeField]
        protected DialogAnimationEvents _dialogAnimationEvents;

        private void Start()
        {
            _dialogAnimationEvents.OnDialogClosedAnimationComplete += DialogAnimationEventsOnDialogClosedAnimationComplete;
        }

        private void DialogAnimationEventsOnDialogClosedAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}