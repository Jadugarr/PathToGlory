using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ActionTimeAddedSystem : GameReactiveSystem
{
    private GameContext context;

    public ActionTimeAddedSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.ExecutionTime, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.SetNewSubstate(SubState.Waiting);
    }
}