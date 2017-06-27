using System;
using Entitas;

public class CleanupDisplayUISystem : ICleanupSystem
{
    private IGroup<GameEntity> uiComponentGroup;

    public CleanupDisplayUISystem(GameContext context)
    {
        uiComponentGroup = context.GetGroup(GameMatcher.UI);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in uiComponentGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}