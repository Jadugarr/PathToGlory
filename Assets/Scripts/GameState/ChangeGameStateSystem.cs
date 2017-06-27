using System.Collections.Generic;
using Entitas;

public class ChangeGameStateSystem : ReactiveSystem<GameEntity>
{
    private GameController gameController;
    private GameState currentGameState;

    public ChangeGameStateSystem(IContext<GameEntity> context, GameController gameController) : base(context)
    {
        this.gameController = gameController;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            gameController.ChangeState(gameEntity.gameState.CurrentGameState);
        }
    }
}