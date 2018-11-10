using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using UnityEngine;

public class EnterChooseActionStateSystem : GameReactiveSystem
{
    private GameContext context;

    public EnterChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ChooseAction))
        {
            CreateChooseActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseAction));
    }

    private void CreateChooseActionSystems()
    {
        Systems chooseActionSystems = new Feature("ChooseActionSystems")
            .Add(new InitializeChooseActionSystem(context))
            .Add(new ActionChosenSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ChooseAction, chooseActionSystems);
    }
}