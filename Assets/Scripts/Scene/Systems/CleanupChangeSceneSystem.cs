using Entitas;

public class CleanupChangeSceneSystem : ICleanupSystem
{
    private IGroup<GameEntity> sceneChangeGroup;

    public CleanupChangeSceneSystem(GameContext context)
    {
        sceneChangeGroup = context.GetGroup(GameMatcher.ChangeScene);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in sceneChangeGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}