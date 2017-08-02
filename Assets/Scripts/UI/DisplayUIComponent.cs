using Entitas;
using UnityEngine;

[Game]
public class DisplayUIComponent : IComponent
{
    public GameObject ViewToDisplay;
    public UiComponentType UiComponentType;
}