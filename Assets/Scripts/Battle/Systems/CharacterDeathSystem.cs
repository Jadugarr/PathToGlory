using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterDeathSystem : ReactiveSystem<GameEntity>
{
    public CharacterDeathSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
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

            gameEntity.death.DeadCharacter.Destroy();
            gameEntity.Destroy();
        }
    }
}