using System.Collections.Generic;
using Entitas;

public class ProcessPauseInputSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ProcessPauseInputSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Pause;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.Paused);
    }
}