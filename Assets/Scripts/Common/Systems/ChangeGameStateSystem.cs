using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ChangeGameStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;

    public ChangeGameStateSystem(IContext<GameEntity> context) : base(context)
    {
        _context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeGameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _context.SetNewGamestate(entities[0].changeGameState.NewGameState);
        entities[0].Destroy();
    }
}