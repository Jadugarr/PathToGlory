using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DisplayUISystem : ReactiveSystem<GameEntity>
{
    private GameObject uiLayer;

    public DisplayUISystem(IContext<GameEntity> context) : base(context)
    {
        uiLayer = GameObject.FindGameObjectWithTag(Tags.UILayer);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.UI);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            Object.Instantiate(gameEntity.uI.ViewToDisplay, uiLayer.transform);
        }
    }
}