using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitBattleWonStateSystem : ReactiveSystem<GameEntity>
{
    public ExitBattleWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.PlayerWon;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Systems playerWonSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerWon);
        if (playerWonSystems != null)
        {
            GameSystemService.RemoveActiveSystems(playerWonSystems);
        }
    }
}