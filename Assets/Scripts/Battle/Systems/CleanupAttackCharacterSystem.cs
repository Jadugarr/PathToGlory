using Entitas;

public class CleanupAttackCharacterSystem : ICleanupSystem
{
    private GameContext context;
    private IGroup<GameEntity> attackCharacterGroup;

    public CleanupAttackCharacterSystem(GameContext gameContext)
    {
        context = gameContext;
        attackCharacterGroup = context.GetGroup(GameMatcher.AttackCharacter);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in attackCharacterGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}