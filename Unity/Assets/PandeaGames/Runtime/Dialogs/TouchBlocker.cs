using UnityEngine;

public interface ITouchBlocker
{
    void Open(bool shouldAnimate);
    void Close(bool shouldAnimate);
}

[RequireComponent(typeof(Animator))]
public  class TouchBlocker : MonoBehaviour, ITouchBlocker
{
    private class TouchBlockerAnimationIDs
    {
        public readonly int OpenTriggerId;
        public readonly int CloseTriggerId;
        public readonly int IdleTriggerId;

        public TouchBlockerAnimationIDs(
            int openTriggerId,
            int closeTriggerId,
            int idleTriggerId
            )
        {
            OpenTriggerId = openTriggerId;
            CloseTriggerId = closeTriggerId;
            IdleTriggerId = idleTriggerId;
        }
    }
    
    private static TouchBlockerAnimationIDs _TouchBlockerAnimationIDs;
    private static TouchBlockerAnimationIDs AnimationsIds
    {
        get
        {
            if (_TouchBlockerAnimationIDs == null)
            {
                _TouchBlockerAnimationIDs = new TouchBlockerAnimationIDs(
                    Animator.StringToHash(OpenTriggerName),
                    Animator.StringToHash(CloseTriggerName),
                    Animator.StringToHash(IdleTriggerName));
            }

            return _TouchBlockerAnimationIDs;
        }
    }
    
    private const string OpenTriggerName = "Open";
    private const string CloseTriggerName = "Close";
    private const string IdleTriggerName = "Idle";
    
    [SerializeField]
    private Animator _animator;

    public virtual void Open(bool shouldAnimate)
    {
        _animator.ResetTrigger(AnimationsIds.CloseTriggerId);
        _animator.SetTrigger(AnimationsIds.OpenTriggerId);
    }

    public virtual void Close(bool shouldAnimate)
    {
        _animator.ResetTrigger(AnimationsIds.OpenTriggerId);
        _animator.SetTrigger(AnimationsIds.CloseTriggerId);
    }
}