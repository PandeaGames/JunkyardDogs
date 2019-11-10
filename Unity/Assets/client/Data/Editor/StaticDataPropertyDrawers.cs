using UnityEditor;
using JunkyardDogs.Data;
using UnityEngine;

public abstract class AbstractStaticDataPropertyDrawer<TAttr> : StaticDataReferencePropertyDrawer where TAttr : StaticDataReferenceAttribute, new()
{
    private static TAttr attribute = new TAttr();
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label, attribute);
    }
}

[CustomPropertyDrawer(typeof(NationalityStaticDataReference), useForChildren:true)]
public class NationalityStaticDataPropertyDrawer : AbstractStaticDataPropertyDrawer<NationalityStaticDataReferenceAttribute>
{
}

[CustomPropertyDrawer(typeof(CurrencyStaticDataReference), useForChildren:true)]
public class CurrencyStaticDataPropertyDrawer : AbstractStaticDataPropertyDrawer<CurrencyStaticDataReferenceAttribute>
{
}

    

