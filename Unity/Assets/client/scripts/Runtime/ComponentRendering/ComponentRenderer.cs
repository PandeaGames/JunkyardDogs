using UnityEngine;
using System.Collections;
using Component = JunkyardDogs.Components.Component;

public abstract class ComponentRenderer : MonoBehaviour
{
    public abstract void RenderComponent(Component component);
}
