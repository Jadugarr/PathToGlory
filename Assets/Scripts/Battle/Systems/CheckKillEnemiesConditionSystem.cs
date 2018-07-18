using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckKillEnemiesConditionSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> enemyEntities;
    private GameContext context;

    public CheckKillEnemiesConditionSystem(GameContext context) : base(context)
    {
        this.context = context;
        enemyEntities = context.GetGroup(GameMatcher.Enemy);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Death);
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity dyingCharacter = context.GetEntityWithId(entity.death.DeadCharacterId);
        return dyingCharacter.isEnemy && enemyEntities.count == 1;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("All enemies are dead!");
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.BattleEnd);
    }
}