using UnityEngine;
using JunkyardDogs.Components;

public abstract class ComponentRenderer : MonoBehaviour
{
    public abstract void RenderComponent(IComponent component);
}
