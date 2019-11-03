using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimationEvents : MonoBehaviour
{
    public event Action OnDialogClosedAnimationComplete;
    
    public void DialogClosed()
    {
        OnDialogClosedAnimationComplete?.Invoke();
    }
}
