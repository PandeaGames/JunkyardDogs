using UnityEngine;
using PandeaGames.ViewModels;
using System;
using UnityEngine.UI;

public interface IDialog
{
    event Action<IDialog> OnFocus;
    event Action<IDialog> OnBlur;
    event Action<IDialog> OnShow;
    event Action<IDialog> OnHide;
    event Action<IDialog> OnClose;
    event Action<IDialog> OnCancel;
    
    void Focus();
    void Blur();
    void Show();
    void Hide();
    void Close();
    void Cancel();
    void Destroy();
    void Setup(IViewModel vm);
}

public abstract class Dialog<TViewModel> : MonoBehaviour, IDialog where TViewModel:AbstractDialogViewModel<TViewModel>, IViewModel
{
    public event Action<IDialog> OnFocus;
    public event Action<IDialog> OnBlur;
    public event Action<IDialog> OnShow;
    public event Action<IDialog> OnHide;
    public event Action<IDialog> OnClose;
    public event Action<IDialog> OnCancel;

    [SerializeField]
    protected Button _closeButton;

    [NonSerialized]
    protected TViewModel _viewModel;
    
    [SerializeField]
    protected Animator _animator;

    public void Setup(IViewModel viewModel)
    {
        _viewModel = viewModel as TViewModel;
        
        if (_closeButton)
        {
            _closeButton.onClick.AddListener(Close);
        }

        Initialize();
    }

    public TViewModel GetViewModel()
    {
        return _viewModel;
    }

    protected virtual void Initialize()
    {
        
    }
    

    public void OnDestroy()
    {
        if (OnFocus != null)
        {
            foreach (Delegate d in OnFocus.GetInvocationList())
                OnFocus -= (Action<IDialog>)d;
            OnFocus = null;
        }

        if (OnBlur != null)
        {
            foreach (Delegate d in OnBlur.GetInvocationList())
                OnBlur -= (Action<IDialog>)d;
            OnBlur = null;
        }

        if (OnShow != null)
        {
            foreach (Delegate d in OnShow.GetInvocationList())
                OnShow -= (Action<IDialog>)d;
            OnShow = null;
        }

        if (OnHide != null)
        {
            foreach (Delegate d in OnHide.GetInvocationList())
                OnHide -= (Action<IDialog>)d;
            OnHide = null;
        }

        if (OnClose != null)
        {
            foreach (Delegate d in OnClose.GetInvocationList())
                OnClose -= (Action<IDialog>)d;
            OnClose = null;
        }

        if (OnCancel != null)
        {
            foreach (Delegate d in OnCancel.GetInvocationList())
                OnCancel -= (Action<IDialog>)d;
            OnCancel = null;
        }
    }

    public void Focus()
    {
        if (OnFocus != null)
            OnFocus(this);
        
        _viewModel.Focus();
    }

    public void Blur()
    {
        if (OnBlur != null)
            OnBlur(this);
        
        _viewModel.Blur();
    }

    public void Show()
    {
        if (OnShow != null)
            OnShow(this);
        
        _viewModel.Show();
    }

    public void Hide()
    {
        if (OnHide != null)
            OnHide(this);
        
        _viewModel.Hide();
    }

    public void Close()
    {
        if (OnClose != null)
            OnClose(this);
        
        _viewModel.Close();
    }

    public void Cancel()
    {
        if (OnCancel != null)
            OnCancel(this);
        
        _viewModel.Cancel();
    }

    public void Destroy()
    {
        _animator.SetTrigger("Close");
        Destroy(this);
    }
}