using System.Collections.Generic;
using Entitas;

public class DisplayUISystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;

    public DisplayUISystem(IContext<GameEntity> context) : base(context)
    {
        gameContext = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DisplayUI);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
    }
}