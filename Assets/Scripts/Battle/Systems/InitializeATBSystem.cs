using Entitas;
using UnityEngine;

public class InitializeATBSystem : IInitializeSystem
{
    private GameContext context;

    public InitializeATBSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        GameObject atbGameObject = Resources.Load("ATB") as GameObject;
        context.CreateEntity()
            .AddDisplayUI(atbGameObject, UiComponentType.Dynamic);
    }
}