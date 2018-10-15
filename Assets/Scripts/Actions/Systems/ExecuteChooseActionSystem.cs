using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecuteChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    private Queue<GameEntity> executeActionQueue = new Queue<GameEntity>();

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

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.ChooseAction);
    }
}