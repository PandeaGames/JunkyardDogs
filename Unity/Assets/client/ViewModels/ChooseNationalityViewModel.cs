using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using PandeaGames.ViewModels;
using PandeaGames.Data.WeakReferences;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

public class ChooseNationalityViewModel : AbstractViewModel
{
    public struct Data
    {
        public readonly List<NationalityStaticDataReference> NationList;
        public readonly JunkyardUser User;
        
        public Data(List<NationalityStaticDataReference> nationList, 
            JunkyardUser user)
        {
            NationList = nationList;
            User = user;
        }
    }
    
    public event Action<NationalityStaticDataReference> OnNationChanged;
    public event Action<NationalityStaticDataReference> OnRequestNationChange;

    private Data _data;
    
    private NationalityStaticDataReference _chosenNationality;
    public NationalityStaticDataReference ChosenNationality
    {
        get { return _chosenNationality; }
    }
    
    public List<NationalityStaticDataReference> NationList
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

    public void RequestChosenNationality(NationalityStaticDataReference nationlity)
    {
        Debug.Log("RequestChosenNationality: "+nationlity.ID);
        OnRequestNationChange?.Invoke(nationlity);
    }

    public void SetChosenNationality(NationalityStaticDataReference nationlity)
    {
        Debug.Log("SetChosenNationality: "+nationlity.ID);
        _chosenNationality = nationlity;

        if (OnNationChanged != null)
            OnNationChanged(nationlity);
    }
}
