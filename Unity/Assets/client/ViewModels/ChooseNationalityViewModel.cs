using System;
using PandeaGames.ViewModels;
using PandeaGames.Data.WeakReferences;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class ChooseNationalityViewModel : AbstractViewModel
{
    public struct Data
    {
        public readonly NationList NationList;
        public readonly JunkyardUser User;
        
        public Data(NationList nationList, 
            JunkyardUser user)
        {
            NationList = nationList;
            User = user;
        }
    }
    
    public event Action<WeakReference> OnNationChanged;

    private Data _data;
    
    private WeakReference _chosenNationality;
    public WeakReference ChosenNationality
    {
        get { return _chosenNationality; }
    }
    
    public NationList NationList
    {
        get { return _data.NationList; }
    }

    public JunkyardUser User
    {
        get
        {
            return _data.User;
        }
    }

    public void SetData(Data data)
    {
        _data = data;
    }

    public void SetChosenNationality(WeakReference nationlity)
    {
        _chosenNationality = nationlity;

        if (OnNationChanged != null)
            OnNationChanged(nationlity);
    }
}
