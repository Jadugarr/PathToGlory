using System.Collections.Generic;
using Entitas;

public class ChangeGameStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ChangeGameStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState != entity.gameState.CurrentGameState;
    }

    protected override void Execute(List<GameEntity> entities)
    {
    }
}