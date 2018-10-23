using System.Collections.Generic;
using Entitas;

public class ProcessBattleCancelInputSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ProcessBattleCancelInputSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity != null && entity.input != null && entity.input.InputCommand == InputCommand.CancelAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceSubState(context.subState.CurrentSubState, context.subState.PreviousSubState);
    }
}