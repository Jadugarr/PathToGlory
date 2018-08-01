using System.Collections.Generic;
using Entitas;

public class WinConditionControllerSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public WinConditionControllerSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WinCondition);
    }

    protected override bool Filter(GameEntity entity)
    {
        bool everythingFulfilled = true;

        foreach (WinConditionState currentWinCondition in entity.winCondition.WinConditions)
        {
            if (currentWinCondition.IsFulfilled == false)
            {
                everythingFulfilled = false;
            }
        }

        return everythingFulfilled;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity conditionsFulfilledEntity = context.CreateEntity();
        conditionsFulfilledEntity.isWinConditionsFulfilled = true;
    }
}