using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitExecuteActionStateSystem : ReactiveSystem<GameEntity>
{
    public ExitExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ExecuteAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.ExecuteAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ExecuteAction));
        }
    }
}