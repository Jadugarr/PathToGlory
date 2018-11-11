using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class GameReactiveSystem : ReactiveSystem<GameEntity>
{
    public GameReactiveSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected abstract override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context);

    protected abstract override bool Filter(GameEntity entity);

    protected sealed override void Execute(List<GameEntity> entities)
    {
//        if (GameSystemService.isSwitchingActiveSystems)
//        {
//            return;
//        }

        ExecuteSystem(entities);
    }

    protected abstract void ExecuteSystem(List<GameEntity> entities);
}