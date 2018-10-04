using System.Collections.Generic;
using Entitas;

public class ActionFinishedSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ActionFinishedSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ActionFinished);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity actionEntity in entities)
        {
            actionEntity.ReplaceBattleAction(actionEntity.battleAction.EntityId, ActionType.ChooseAction,
                ActionATBType.Waiting);
            actionEntity.ReplaceExecutionTime(10f, 10f);
        }
    }
}