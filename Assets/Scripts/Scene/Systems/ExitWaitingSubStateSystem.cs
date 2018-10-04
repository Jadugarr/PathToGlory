using System.Collections.Generic;
using Entitas;

public class ExitWaitingSubStateSystem : ReactiveSystem<GameEntity>
{
    public ExitWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.Waiting;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        if (waitSystems != null)
        {
            GameSystemService.RemoveActiveSystems(waitSystems);
        }
    }
}