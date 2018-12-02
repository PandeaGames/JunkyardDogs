namespace PandeaGames.ViewModels
{
    public abstract class AbstractDialogViewModel<TViewModelType> : IViewModel where TViewModelType:class, IViewModel
    {
        public delegate void DialogViewModelDelegate(TViewModelType dialog);

        public event DialogViewModelDelegate OnFocus;
        public event DialogViewModelDelegate OnBlur;
        public event DialogViewModelDelegate OnShow;
        public event DialogViewModelDelegate OnHide;
        public event DialogViewModelDelegate OnClose;
        public event DialogViewModelDelegate OnCancel;
        
        public void Reset()
        {
            
        }
        
        public void Focus()
        {
            if (OnFocus != null)
                OnFocus(this as TViewModelType);
        }

        public void Blur()
        {
            if (OnBlur != null)
                OnBlur(this as TViewModelType);
        }

        public void Show()
        {
            if (OnShow != null)
                OnShow(this as TViewModelType);
        }

        public void Hide()
        {
            if (OnHide != null)
                OnHide(this as TViewModelType);
        }

        public void Close()
        {
            if (OnClose != null)
                OnClose(this as TViewModelType);
        }

        public void Cancel()
        {
            if (OnCancel != null)
                OnCancel(this as TViewModelType);
        }
    }
}