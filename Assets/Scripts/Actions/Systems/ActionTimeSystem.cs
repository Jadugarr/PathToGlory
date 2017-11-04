using Entitas;
using UnityEngine;

public class ActionTimeSystem : IExecuteSystem
{
    private GameContext context;
    private IGroup<GameEntity> actionEntities;
    private IGroup<GameEntity> readyEntities;

    public ActionTimeSystem(IContext<GameEntity> context)
    {
        this.context = (GameContext) context;
        actionEntities = this.context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
        readyEntities = this.context.GetGroup(GameMatcher.ExecuteAction);
    }

    public void Execute()
    {
        if (readyEntities.count == 0)
        {
            foreach (GameEntity actionEntity in actionEntities.GetEntities())
            {
                GameEntity performingCharacter = context.GetEntityWithId(actionEntity.battleAction.EntityId);
                float newRemainingTime = actionEntity.executionTime.RemainingTime -
                                         Time.deltaTime * BattleUtils.GetActionTimeStep(
                                             actionEntity.battleAction.ActionType,
                                             performingCharacter.speed.SpeedValue);
                actionEntity.ReplaceExecutionTime(actionEntity.executionTime.TotalTime,
                    newRemainingTime);

                if (newRemainingTime <= 0)
                {
                    actionEntity.isExecuteAction = true;
                }
            }
        }
    }
}