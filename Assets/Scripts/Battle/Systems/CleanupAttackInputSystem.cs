using Entitas;

public class CleanupAttackInputSystem : ICleanupSystem
{
    private IGroup<GameEntity> attackInputGroup;

    public CleanupAttackInputSystem(GameContext context)
    {
        attackInputGroup = context.GetGroup(GameMatcher.AttackInput);
    }

    public void Cleanup()
    {
        GameEntity[] entities = attackInputGroup.GetEntities();

        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.Destroy();
        }
    }
}