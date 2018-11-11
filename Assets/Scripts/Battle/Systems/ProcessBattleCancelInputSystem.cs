using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ProcessBattleCancelInputSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.ReplaceChangeSubState(context.subState.PreviousSubState);
    }
}