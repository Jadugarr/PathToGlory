using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckPlayerDeadConditionSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> playerEntities;
    private GameContext context;

    public CheckPlayerDeadConditionSystem(IContext<GameEntity> context) : base(context)
    {
        playerEntities = context.GetGroup(GameMatcher.Player);
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Player, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return playerEntities.count == 0;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("All player characters are dead!");
        LoseConditionComponent loseConditions = context.loseCondition;

        for (var i = 0; i < loseConditions.LoseConditions.Length; i++)
        {
            LoseConditionState currentLoseCondition = loseConditions.LoseConditions[i];
            if (currentLoseCondition.LoseCondition == LoseCondition.PlayerDead)
            {
                loseConditions.LoseConditions[i].IsFulfilled = true;
                break;
            }
        }

        context.ReplaceLoseCondition(context.loseCondition.ConditionModifier, loseConditions.LoseConditions);
    }
}