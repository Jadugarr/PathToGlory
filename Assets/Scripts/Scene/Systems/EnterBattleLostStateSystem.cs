using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using UnityEngine;

public class EnterBattleLostStateSystem : ReactiveSystem<GameEntity>
{
    public EnterBattleLostStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerLost;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.PlayerLost))
        {
            CreatePlayerLostSystems();
        }

        Systems playerLostSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerLost);
        GameSystemService.AddActiveSystems(playerLostSystems);
    }

    private void CreatePlayerLostSystems()
    {
        Systems playerLostSystems = new Feature("PlayerLostSystems")
            .Add(new DisplayBattleLostSystem());

        GameSystemService.AddSubSystemMapping(SubState.PlayerLost, playerLostSystems);
    }
}