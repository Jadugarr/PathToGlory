using System.Collections.Generic;
using Entitas;

public class ReleaseDefenseActionSystem : ReactiveSystem<GameEntity>
{
    public ReleaseDefenseActionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecuteAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDefend;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.isDefend = false;
        }
    }
}