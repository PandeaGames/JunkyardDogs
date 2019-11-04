using JunkyardDogs.Data;
using UnityEngine;
using UnityEngine.UI;

public class NationalityDisplay : AbstractListItem<NationalityStaticDataReference>
{
    [SerializeField]
    private Image _image;
    
    [SerializeField]
    private SpriteFactory _nationFlagFactory;

    public override void SetData(NationalityStaticDataReference data)
    {
        base.SetData(data);
        Nationality nationality = data.Data;
        _image.sprite = _nationFlagFactory.GetAsset(nationality);
    }
}
