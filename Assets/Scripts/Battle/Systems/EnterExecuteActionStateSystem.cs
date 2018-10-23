using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EnterExecuteActionStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override void Execute(List<GameEntity> entities)
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
            .Add(new ExecutePlayerAttackActionSystem(context))
            .Add(new ExecuteDefenseActionSystem(context))
            .Add(new ReleaseDefenseActionSystem(context))
            .Add(new ActionFinishedSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ExecuteAction, executeActionSystems);
    }
}