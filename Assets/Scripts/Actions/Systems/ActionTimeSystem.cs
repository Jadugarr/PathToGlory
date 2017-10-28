using Entitas;
using UnityEngine;

public class ActionTimeSystem : IExecuteSystem
{
    private GameContext context;
    private IGroup<GameEntity> actionEntities;

    public ActionTimeSystem(IContext<GameEntity> context)
    {
        this.context = (GameContext) context;
        actionEntities = this.context.GetGroup(GameMatcher.BattleAction);
    }

    public void Execute()
    {
        foreach (GameEntity gameEntity in actionEntities.GetEntities())
        {
            // TODO: I can't subtract the time like this, because I need to factor in the character's speed, which will be a dynamic value
            gameEntity.battleAction.RemainingTimeToExecution -= Time.deltaTime;
        }
    }
}