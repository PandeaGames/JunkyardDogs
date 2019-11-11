using UnityEngine;



public class AnimatedProgressBarBehvaiour : AbstractProgressDisplay
{
    private class ProgressBarAnimationIDs
    {
        public readonly int FillTriggerID;
        public readonly int EmptyTriggerID;
        public readonly int FillingTriggerId;

        public ProgressBarAnimationIDs(
            int fillTriggerId,
            int emptyTriggerId,
            int fillingTriggerId
        )
        {
            FillTriggerID = fillTriggerId;
            EmptyTriggerID = emptyTriggerId;
            FillingTriggerId = fillingTriggerId;
        }
    }
    
    private static ProgressBarAnimationIDs _ProgressBarAnimationIDs;
    private static ProgressBarAnimationIDs AnimationsIds
    {
        get
        {
            if (_ProgressBarAnimationIDs == null)
            {
                _ProgressBarAnimationIDs = new ProgressBarAnimationIDs(
                    Animator.StringToHash(FillTriggerName),
                    Animator.StringToHash(EmptyTriggerName),
                    Animator.StringToHash(FillingTriggerName));
            }

            return _ProgressBarAnimationIDs;
        }
    }
    
    private const string FillTriggerName = "Full";
    private const string EmptyTriggerName = "Empty";
    private const string FillingTriggerName = "Filling";
    
    [SerializeField]
    private Animator _animator;
    
    public override void SetProgress(float percentage)
    {
        if (percentage <= float.Epsilon)
        {
            //_animator.SetTrigger(AnimationsIds.EmptyTriggerID);
            _animator.Play(AnimationsIds.EmptyTriggerID, 0, 0);
        }
        else if (percentage >= 1 - float.Epsilon)
        {
            //_animator.SetTrigger(AnimationsIds.FillTriggerID);
            _animator.Play(AnimationsIds.FillTriggerID, 0, 0);
        }
        else
        {
            _animator.SetTrigger(AnimationsIds.FillingTriggerId);
            _animator.Play(AnimationsIds.FillingTriggerId, 0, percentage);
        }
    }
}
