using System.Collections.Generic;
using Entitas;

public class ExecuteDefenseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExecuteDefenseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecuteAction, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity.hasBattleAction)
        {
            return entity.battleAction.ActionType == ActionType.Defend;
        }

        return false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity defendingCharacter = context.GetEntityWithId(gameEntity.battleAction.EntityId);
            defendingCharacter.isDefend = true;

            gameEntity.isExecuteAction = false;
            gameEntity.isActionFinished = true;
        }
    }
}