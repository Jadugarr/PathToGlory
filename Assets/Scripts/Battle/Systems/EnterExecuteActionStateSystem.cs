using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EnterExecuteActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.ExecuteAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public EnterExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ExecuteAction))
        {
            CreateExecuteActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ExecuteAction));
    }

    private void CreateExecuteActionSystems()
    {
        Systems executeActionSystems = new Feature("ExecuteActionSystems")
            //Actions
            .Add(new ExecutePlayerAttackActionSystem(_context))
            .Add(new ExecuteDefenseActionSystem(_context))
            .Add(new ReleaseDefenseActionSystem(_context))
            .Add(new ActionFinishedSystem(_context));

        GameSystemService.AddSubSystemMapping(SubState.ExecuteAction, executeActionSystems);
    }
}