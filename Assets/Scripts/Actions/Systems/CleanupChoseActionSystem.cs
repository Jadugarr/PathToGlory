using Entitas;

public class CleanupChoseActionSystem : ICleanupSystem
{
    private IGroup<GameEntity> choseActionGroup;

    public CleanupChoseActionSystem(GameContext context)
    {
        choseActionGroup = context.GetGroup(GameMatcher.ChoseAction);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in choseActionGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}