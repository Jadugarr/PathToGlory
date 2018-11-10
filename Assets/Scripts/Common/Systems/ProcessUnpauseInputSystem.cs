using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessUnpauseInputSystem : GameReactiveSystem
{
    private GameContext context;

    public ProcessUnpauseInputSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Unpause;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.SetNewSubstate(context.subState.PreviousSubState);
    }
}