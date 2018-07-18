using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BattleEndSystem : ReactiveSystem<GameEntity>
{
    public BattleEndSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Id);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }
}