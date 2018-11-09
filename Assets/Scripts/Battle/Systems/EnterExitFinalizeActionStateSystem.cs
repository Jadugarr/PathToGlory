using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;

public class EnterFinalizeActionStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.FinalizeAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.FinalizeAction))
        {
            CreateFinalizeActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.FinalizeAction));
    }

    private void CreateFinalizeActionSystems()
    {
        Systems finalizeActionSystems = new Feature("finalizeActionSystems")
            .Add(new AddActionTimeSystem(context))
            .Add(new ActionTimeAddedSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.FinalizeAction, finalizeActionSystems);
    }
}

public class ExitFinalizeActionStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.FinalizeAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.FinalizeAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.FinalizeAction));
        }
    }
}