using System.Collections.Generic;
using Entitas;

public class ChangeGameStateSystem : GameReactiveSystem
{

    public ChangeGameStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState != entity.gameState.CurrentGameState;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            InputConfiguration.ChangeActiveGameStateInputMap(gameEntity.gameState.CurrentGameState);
        }
    }
}