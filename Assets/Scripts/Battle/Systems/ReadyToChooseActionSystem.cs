using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ReadyToChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ReadyToChooseActionSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReadyToChooseAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity readyToActEntity = context.GetEntityWithId(gameEntity.readyToChooseAction.EntityReadyToActId);

            if (readyToActEntity.isEnemy)
            {
                Debug.Log("Skipping enemy turn!");
                readyToActEntity.ReplaceTimeUntilAction(10f, 10f);
                gameEntity.Destroy();
            }
        }
    }
}