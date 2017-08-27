using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ReadyToActSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ReadyToActSystem(GameContext context) : base(context)
    {
        this.context = context;
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
            GameEntity readyToActEntity = context.GetEntityWithId(gameEntity.readyToAct.EntityReadyToActId);

            if (readyToActEntity.isEnemy)
            {
                Debug.Log("Skipping enemy turn!");
                gameEntity.ReplaceTimeUntilAction(10f, 10f);
                gameEntity.Destroy();
            }
        }
    }
}