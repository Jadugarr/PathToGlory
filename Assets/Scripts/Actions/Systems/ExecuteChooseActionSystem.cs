using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ExecuteChooseActionSystem : GameReactiveSystem
{
    private GameContext context;

    public ExecuteChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecutionTime, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionType == ActionType.ChooseAction && entity.executionTime.RemainingTime <= 0f;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.ReplaceChangeSubState(SubState.ChooseAction);
    }
}