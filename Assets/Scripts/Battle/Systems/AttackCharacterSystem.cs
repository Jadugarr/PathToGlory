using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AttackCharacterSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public AttackCharacterSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AttackCharacter);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }
}