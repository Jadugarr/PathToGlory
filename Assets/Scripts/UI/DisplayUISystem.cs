using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DisplayUISystem : ReactiveSystem<GameEntity>
{
    private GameObject uiLayer;
    private GameContext gameContext;

    public DisplayUISystem(IContext<GameEntity> context) : base(context)
    {
        uiLayer = GameObject.FindGameObjectWithTag(Tags.UILayer);
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
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity newEntity = gameContext.CreateEntity();
            newEntity.AddView(Object.Instantiate(gameEntity.displayUI.ViewToDisplay, uiLayer.transform));
            newEntity.isUI = true;
        }
    }
}