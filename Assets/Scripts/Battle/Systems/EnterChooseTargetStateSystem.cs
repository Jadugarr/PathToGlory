using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;

public class EnterChooseTargetStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterChooseTargetStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseTarget;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ChooseTarget))
        {
            CreateChooseTargetSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseTarget));
    }

    private void CreateChooseTargetSystems()
    {
        Systems chooseTargetSystems = new Feature("ChooseTargetSystems")
            .Add(new InitializeChooseTargetSystem(context))
            .Add(new ActionTargetChosenSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ChooseTarget, chooseTargetSystems);
    }
}