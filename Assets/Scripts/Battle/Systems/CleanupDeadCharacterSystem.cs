using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CleanupDeadCharacterSystem : ICleanupSystem
{
    private IGroup<GameEntity> deadCharacters;
    private GameContext context;

    public CleanupDeadCharacterSystem(GameContext context)
    {
        this.context = context;

        deadCharacters = context.GetGroup(GameMatcher.Death);
    }

    public void Cleanup()
    {
        foreach (GameEntity deadCharacterReference in deadCharacters.GetEntities())
        {
            GameEntity characterEntity = context.GetEntityWithId(deadCharacterReference.death.DeadCharacterId);

            if (characterEntity.hasView)
            {
                GameObject.Destroy(characterEntity.view.View);
            }

            characterEntity.Destroy();
            deadCharacterReference.Destroy();
        }
    }
}