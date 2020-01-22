using JunkyardDogs.Data;
using PandeaGames;
using UnityEngine;
using UnityEngine.UI;

public class NationalityDisplay : AbstractListItem<NationalityStaticDataReference>
{
    [SerializeField]
    private Image _image;
    
    [SerializeField] private SynchronousStaticDataProvider.NationalityImageTypes _imageType;

    public override void SetData(NationalityStaticDataReference data)
    {
        base.SetData(data);
        Nationality nationality = data.Data;
        _image.sprite = SynchronousStaticDataProvider.Instance.GetData(_imageType, nationality);
    }
    
    public void SetData(NationalityStaticDataReference data, string titleOverride)
    {
        SetData(data);

        if (_title != null)
        {
            _title.text = titleOverride;
        }
    }
    
    protected override string GetName(NationalityStaticDataReference item)
    {
        return item.ID;
    }
}
