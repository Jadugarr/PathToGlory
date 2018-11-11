using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class LoseConditionControllerSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.ReplaceChangeSubState(SubState.PlayerLost);
    }
}