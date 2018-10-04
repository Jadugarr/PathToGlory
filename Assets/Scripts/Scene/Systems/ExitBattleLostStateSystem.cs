using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitBattleLostStateSystem : ReactiveSystem<GameEntity>
{
    public ExitBattleLostStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.PlayerLost;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Systems playerLostSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerLost);
        GameSystemService.RemoveActiveSystems(playerLostSystems);
    }
}