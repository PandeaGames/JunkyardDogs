using UnityEngine;

public class RTProgressBarBehaviour : AbstractProgressDisplay
{
    [SerializeField]
    private RectTransform _fill;
    
    [SerializeField]
    private RectTransform _container;

    public override void SetProgress(float percentage)
    {
        percentage = Mathf.Min(percentage, 1);
        _fill.anchorMax = new Vector2(0, 1);
        _fill.anchorMin = new Vector2(0, 0);
            
        _fill.offsetMax = new Vector2(_container.rect.width * percentage, 0);
        _fill.offsetMin = new Vector2(0,0);

        _fill.pivot = new Vector2(0, 0);
    }
}
