using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecutePlayerAttackActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExecutePlayerAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecuteAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity.hasBattleAction)
        {
            GameEntity executionerEntity = context.GetEntityWithId(entity.battleAction.EntityId);
            return entity.battleAction.ActionType == ActionType.AttackCharacter && executionerEntity.isPlayer;
        }

        return false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity attacker = context.GetEntityWithId(gameEntity.battleAction.EntityId);
            GameEntity defender = context.GetEntityWithId(gameEntity.target.TargetId); ;
            defender.ReplaceHealth(
                defender.health.Health -
                Math.Max(0,
                    attacker.attack.AttackValue -
                    defender.defenseStat.DefenseValue));

            Debug.Log("Enemy attacked! Remaining health: " + defender.health.Health);

            gameEntity.isExecuteAction = false;
            gameEntity.isActionFinished = true;
        }
    }
}