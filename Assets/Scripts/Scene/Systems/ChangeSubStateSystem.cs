using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ChangeSubStateSystem : ReactiveSystem<GameEntity>
{
    public ChangeSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            InputConfiguration.ChangeActiveSubStateInputMap(gameEntity.subState.CurrentSubState);
        }
    }
}