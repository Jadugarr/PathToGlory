using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ReadyToActSystem : ReactiveSystem<GameEntity>
{
    public ReadyToActSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReadyToAct);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.readyToAct.EntityReadyToAct.isEnemy)
            {
                Debug.Log("Skipping enemy turn!");
                gameEntity.ReplaceTimeUntilAction(10f, 10f);
                gameEntity.Destroy();
            }
        }
    }
}