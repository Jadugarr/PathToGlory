using System.Collections.Generic;
using Entitas;

public class EnterWaitingSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.Waiting;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.Waiting))
        {
            CreateWaitingSystems();
        }

        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        waitSystems.ActivateReactiveSystems();
        waitSystems.Initialize();
        GameSystemService.AddActiveSystems(waitSystems);
        InputConfiguration.ChangeActiveSubStateInputMap(SubState.Waiting);
    }

    private void CreateWaitingSystems()
    {
        Systems waitStateSystems = new Systems()
            .Add(new ActionTimeSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.Waiting, waitStateSystems);
    }
}