using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterDeathSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public CharacterDeathSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Death);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.death.DeadCharacter.hasView)
            {
                GameObject.Destroy(gameEntity.death.DeadCharacter.view.View);
            }

            Debug.Log("Enemy died!");

            context.DestroyEntity(gameEntity.death.DeadCharacter);
            context.DestroyEntity(gameEntity);
        }
    }
}