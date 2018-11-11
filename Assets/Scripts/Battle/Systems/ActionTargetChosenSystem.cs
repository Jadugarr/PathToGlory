using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ActionTargetChosenSystem : GameReactiveSystem
{
    private GameContext context;
    
    public ActionTargetChosenSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Target, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.ReplaceChangeSubState(SubState.FinalizeAction);
    }
}