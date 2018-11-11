using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ChangeSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    
    public ChangeSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        _context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeSubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _context.SetNewSubstate(entities[0].changeSubState.NewSubState);
        entities[0].Destroy();
    }
}