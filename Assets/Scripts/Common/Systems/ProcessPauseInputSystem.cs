using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessPauseInputSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.ReplaceChangeSubState(SubState.Paused);
    }
}