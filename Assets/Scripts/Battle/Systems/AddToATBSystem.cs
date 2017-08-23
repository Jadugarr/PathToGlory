using System.Collections.Generic;
using Entitas;

public class AddToATBSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public AddToATBSystem(IContext<GameEntity> context) : base(context)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Battle.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }
}