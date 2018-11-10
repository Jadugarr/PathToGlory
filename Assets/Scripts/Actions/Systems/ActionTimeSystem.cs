using Entitas;
using Entitas.Utils;
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
        if (!GameSystemService.isSwitchingActiveSystems)
        {
            foreach (GameEntity actionEntity in actionEntities)
            {
                GameEntity performingCharacter = context.GetEntityWithId(actionEntity.battleAction.EntityId);
                float newRemainingTime = actionEntity.executionTime.RemainingTime -
                                         Time.deltaTime * BattleActionUtils.GetActionTimeStep(
                                             actionEntity.battleAction.ActionType,
                                             performingCharacter);
                actionEntity.ReplaceExecutionTime(actionEntity.executionTime.TotalTime,
                    newRemainingTime);
            }
        }
    }
}