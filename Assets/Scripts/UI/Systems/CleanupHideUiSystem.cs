using Entitas;

public class CleanupHideUiSystem : ICleanupSystem
{
    private IGroup<GameEntity> entityGroup;

    public CleanupHideUiSystem(GameContext context)
    {
        entityGroup = context.GetGroup(GameMatcher.HideUi);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in entityGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}