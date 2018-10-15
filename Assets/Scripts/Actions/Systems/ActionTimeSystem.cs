using Entitas;
using UnityEngine;

public class ActionTimeSystem : IExecuteSystem
{
    private GameContext context;
    private IGroup<GameEntity> actionEntities;

    public ActionTimeSystem(IContext<GameEntity> context)
    {
        this.context = (GameContext) context;
        actionEntities = this.context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
    }

    public void Execute()
    {
        foreach (GameEntity actionEntity in actionEntities)
        {
            GameEntity performingCharacter = context.GetEntityWithId(actionEntity.battleAction.EntityId);
            float newRemainingTime = actionEntity.executionTime.RemainingTime -
                                     Time.deltaTime * BattleUtils.GetActionTimeStep(
                                         actionEntity.battleAction.ActionType,
                                         performingCharacter.speed.SpeedValue);
            actionEntity.ReplaceExecutionTime(actionEntity.executionTime.TotalTime,
                newRemainingTime);
        }
    }
}