﻿using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecutePlayerAttackActionSystem : GameReactiveSystem
{
    private GameContext context;

    public ExecutePlayerAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity.hasBattleAction)
        {
            return entity.battleAction.ActionType == ActionType.AttackCharacter &&
                   entity.executionTime.RemainingTime < 0f;
        }

        return false;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity attacker = context.GetEntityWithId(gameEntity.battleAction.EntityId);
            GameEntity defender = context.GetEntityWithId(gameEntity.target.TargetId);
            ;
            defender.ReplaceHealth(
                defender.health.Health -
                Math.Max(0,
                    attacker.attack.AttackValue -
                    defender.defenseStat.DefenseValue));

            Debug.Log("Enemy attacked! Remaining health: " + defender.health.Health);

            gameEntity.isActionFinished = true;
        }
    }
}