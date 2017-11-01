using System.Collections.Generic;
using Entitas;

public class ExecuteChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    private Queue<GameEntity> chooseQueue = new Queue<GameEntity>();

    public ExecuteChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext)context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ExecuteAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity actionEntity = context.GetEntityWithId(entity.executeAction.ActionId);
        return actionEntity.battleAction.ActionType == ActionType.ChooseAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            chooseQueue.Enqueue(gameEntity);
        }
    }

    private void ProcessQueue()
    {
        
    }
}