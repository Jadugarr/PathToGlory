using System.Collections.Generic;
using Entitas;

public class ProcessBattleCancelInputSystem : ReactiveSystem<GameEntity>
{
    public ProcessBattleCancelInputSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.CancelAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        ActionBuilder.Instance.Cancel();
    }
}