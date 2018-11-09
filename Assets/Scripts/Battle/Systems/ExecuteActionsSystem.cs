using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ExecuteActionsSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;

    public ExecuteActionsSystem(IContext<GameEntity> context) : base(context)
    {
        _context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecutionTime, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionAtbType == ActionATBType.Acting && entity.executionTime.RemainingTime <= 0f;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.ExecuteAction);
    }
}