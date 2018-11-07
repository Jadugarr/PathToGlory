using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ActionTargetChosenSystem : ReactiveSystem<GameEntity>
{
    public ActionTargetChosenSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Target, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("Target chosen!");
    }
}