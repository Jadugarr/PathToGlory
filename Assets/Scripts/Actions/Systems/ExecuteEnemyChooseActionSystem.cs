using System.Collections.Generic;
using Entitas;

public class ExecuteEnemyChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private Queue<GameEntity> executeActionQueue = new Queue<GameEntity>();
    private bool isExecuting;
    private GameEntity currentActionEntity;

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
        foreach (GameEntity gameEntity in entities)
        {
            executeActionQueue.Enqueue(gameEntity);
        }

        if (isExecuting == false)
        {
            isExecuting = true;
            ProcessQueue();
        }
        //foreach (GameEntity actionEntity in entities)
        //{
        //    BattleActionComponent currentBattleAction = actionEntity.battleAction;
        //    GameEntity newActionEntity = context.CreateEntity();
        //    newActionEntity.AddBattleAction(currentBattleAction.EntityId, ActionType.None,
        //        ActionATBType.Acting);
        //    ActionBuilder.Instance.ChooseActionSequence(newActionEntity, context, OnSuccess, OnError, false);
        //}
    }

    private void ProcessQueue()
    {
        currentActionEntity = executeActionQueue.Dequeue();
        BattleActionComponent currentBattleAction = currentActionEntity.battleAction;
        GameEntity newActionEntity = context.CreateEntity();
        newActionEntity.AddBattleAction(currentBattleAction.EntityId, ActionType.None,
            ActionATBType.Acting);
        ActionBuilder.Instance.ChooseActionSequence(newActionEntity, context, OnSuccess, OnError, false);
    }

    private void OnSuccess(GameEntity entity)
    {
        currentActionEntity.Destroy();

        if (executeActionQueue.Count > 0)
        {
            ProcessQueue();
        }
        else
        {
            isExecuting = false;
        }
    }

    private void OnError(string error)
    {
        // nothing for now
    }
}