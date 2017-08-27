using Entitas;

public class CleanupSceneChangedSystem : ICleanupSystem
{
    private IGroup<GameEntity> sceneChangedGroup;

    public CleanupSceneChangedSystem(GameContext context)
    {
        sceneChangedGroup = context.GetGroup(GameMatcher.SceneChanged);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in sceneChangedGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}