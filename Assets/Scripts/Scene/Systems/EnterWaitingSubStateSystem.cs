using System.Collections.Generic;
using Entitas;

public class EnterWaitingSubStateSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.Waiting))
        {
            CreateWaitingSystems();
        }

        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        GameSystemService.AddActiveSystems(waitSystems);
    }

    private void CreateWaitingSystems()
    {
        Systems waitStateSystems = new Feature("WaitingSubStateSystems")
            .Add(new ActionTimeSystem(context))
            //Actions
            .Add(new ExecuteChooseActionSystem(context))
            .Add(new ExecuteActionsSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.Waiting, waitStateSystems);
    }
}