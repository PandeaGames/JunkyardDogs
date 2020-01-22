using JunkyardDogs.Components;
using JunkyardDogs.Data;
using UnityEngine;

public class LootMonoView : MonoBehaviour
{
    [SerializeField]
    private ComponentDisplay _componentDisplay;
    
    [SerializeField]
    private CurrencyDisplay _currencyDisplay;

    [SerializeField] 
    private DirectiveView _directiveView;

    [SerializeField] 
    private NationalityDisplay _nationalityDisplay;
    
    public virtual void RenderLoot(ILoot loot)
    {
        if(loot is Currency) RenderLoot(loot as Currency);
        else if(loot is Directive) RenderLoot(loot as Directive);
        else if(loot is IComponent) RenderLoot(loot as IComponent);
        else if(loot is NationalExp) RenderLoot(loot as NationalExp);
        else Debug.LogWarning("[LootMonoView] Does not support rendering of loot "+loot.ToString());
    }
    
    protected virtual void RenderLoot(Currency loot)
    {
        _currencyDisplay.DisplayCurrency(loot);
    }
    
    protected virtual void RenderLoot(IComponent loot)
    {
        _componentDisplay.RenderComponent(loot);
    }
    
    protected virtual void RenderLoot(Directive loot)
    {
        _directiveView.SetupComponent(loot);
    }

    protected virtual void RenderLoot(NationalExp exp)
    {
        NationalityStaticDataReference reference = new NationalityStaticDataReference();
        reference.ID = exp.Nationality.ID;
        _nationalityDisplay.SetData(reference, "+ " + exp.Exp);
    }
}
