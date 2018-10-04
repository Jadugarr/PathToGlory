using System.Collections.Generic;
using Entitas;

public class EnterBattleWonStateSystem : ReactiveSystem<GameEntity>
{
    public EnterBattleWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerWon;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (GameSystemService.GetSubSystemMapping(SubState.PlayerWon) == null)
        {
            CreatePlayerWonSystems();
        }
        
        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.PlayerWon));
    }

    private void CreatePlayerWonSystems()
    {
        Systems playerWonSystems = new Feature("PlayerWonSystems")
            .Add(new DisplayBattleWonSystem());
        
        GameSystemService.AddSubSystemMapping(SubState.PlayerWon, playerWonSystems);
    }
}