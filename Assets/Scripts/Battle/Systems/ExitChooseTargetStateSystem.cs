using System.Collections.Generic;
using Entitas;

public class ExitChooseTargetStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitChooseTargetStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ChooseTarget;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.ChooseTarget))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseTarget));
        }
    }
}