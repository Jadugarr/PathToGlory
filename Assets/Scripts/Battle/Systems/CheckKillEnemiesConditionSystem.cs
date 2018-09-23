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
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Enemy, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return enemyEntities.count == 0;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("All enemies are dead!");
        WinConditionComponent winConditions = context.winCondition;

        for (var i = 0; i < winConditions.WinConditions.Length; i++)
        {
            WinConditionState currentWinCondition = winConditions.WinConditions[i];
            if (currentWinCondition.WinCondition == WinCondition.KillEnemies)
            {
                winConditions.WinConditions[i].IsFulfilled = true;
                break;
            }
        }

        context.ReplaceWinCondition(context.winCondition.ConditionModifier, winConditions.WinConditions);
    }
}