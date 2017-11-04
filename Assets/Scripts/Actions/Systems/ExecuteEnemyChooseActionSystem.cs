using System.Collections.Generic;
using Entitas;

public class ExecuteEnemyChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExecuteEnemyChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ExecuteAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity executionerEntity = context.GetEntityWithId(entity.battleAction.EntityId);
        return entity.battleAction.ActionType == ActionType.ChooseAction && executionerEntity.isEnemy;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity actionEntity in entities)
        {
            // Just skipping actions for now
            actionEntity.isActionFinished = true;
        }
    }
}