using System.Collections.Generic;
using Entitas;

public class EnterPausedSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterPausedSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return context.subState.CurrentSubState == SubState.Paused;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        InputConfiguration.ChangeActiveSubStateInputMap(SubState.Paused);
    }
}