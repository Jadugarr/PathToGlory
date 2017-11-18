using System.Collections.Generic;
using Entitas;

public class EnterWaitingSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.Waiting;
    }

    protected override void Execute(List<GameEntity> entities)
    {

    }
}