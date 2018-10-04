using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class LoseConditionControllerSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public LoseConditionControllerSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LoseCondition);
    }

    protected override bool Filter(GameEntity entity)
    {
        bool everythingFulfilled = true;

        foreach (LoseConditionState currentLoseCondition in entity.loseCondition.LoseConditions)
        {
            if (currentLoseCondition.IsFulfilled == false)
            {
                everythingFulfilled = false;
            }
        }

        return everythingFulfilled;
    }

    protected override void Execute(List<GameEntity> entities)
    {context.ReplaceSubState(context.subState.CurrentSubState, SubState.PlayerLost);
    }
}