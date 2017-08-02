using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DisplayUISystem : ReactiveSystem<GameEntity>
{
    private GameObject staticUiLayer;
    private GameObject dynamicUiLayer;
    private GameContext gameContext;

    public DisplayUISystem(IContext<GameEntity> context) : base(context)
    {
        staticUiLayer = GameObject.FindGameObjectWithTag(Tags.StaticUILayer);
        dynamicUiLayer = GameObject.FindGameObjectWithTag(Tags.DynamicUILayer);
        gameContext = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DisplayUI);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameObject parentGameObject;
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.displayUI.UiComponentType == UiComponentType.Static)
            {
                parentGameObject = staticUiLayer;
            }
            else if (gameEntity.displayUI.UiComponentType == UiComponentType.Dynamic)
            {
                parentGameObject = dynamicUiLayer;
            }
            else
            {
                Debug.Log("UI component has no defined UiComponentType and will use static UI layer: " + gameEntity.displayUI.ViewToDisplay);
                parentGameObject = staticUiLayer;
            }
            GameEntity newEntity = gameContext.CreateEntity();
            newEntity.AddView(Object.Instantiate(gameEntity.displayUI.ViewToDisplay, parentGameObject.transform));
            newEntity.isUI = true;
        }
    }
}