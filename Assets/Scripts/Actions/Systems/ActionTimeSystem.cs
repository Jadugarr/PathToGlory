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
        actionEntities = this.context.GetGroup(GameMatcher.BattleAction);
        readyEntities = this.context.GetGroup(GameMatcher.ExecuteAction);
    }

    public void Execute()
    {
        if (readyEntities.count == 0)
        {
            foreach (GameEntity gameEntity in actionEntities.GetEntities())
            {
                GameEntity performingCharacter = context.GetEntityWithId(gameEntity.battleAction.EntityId);
                float newRemainingTime = gameEntity.battleAction.RemainingTimeToExecution -
                                         Time.deltaTime * BattleUtils.GetActionTimeStep(
                                             gameEntity.battleAction.ActionType,
                                             performingCharacter.speed.SpeedValue);
                gameEntity.ReplaceBattleAction(gameEntity.battleAction.EntityId, gameEntity.battleAction.ActionType,
                    gameEntity.battleAction.ActionAtbType, gameEntity.battleAction.ActionProperties,
                    gameEntity.battleAction.TotalTimeToExecution, newRemainingTime);

                if (newRemainingTime <= 0)
                {
                    GameEntity executeActionEntity = context.CreateEntity();
                    executeActionEntity.AddExecuteAction(gameEntity.id.Id);
                }
            }
        }
    }
}