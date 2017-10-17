using System.Collections.Generic;
using Entitas;

public class ExecuteAttackActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExecuteAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext)context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChoseAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.choseAction.ActionType == ActionType.AttackCharacter;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity attackInputEntity = context.CreateEntity();
        attackInputEntity.isAttackInput = true;
    }
}