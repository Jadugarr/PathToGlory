using Entitas;

public class CleanupChoseCharacterSystem : ICleanupSystem
{
    private IGroup<GameEntity> choseCharacterGroup;

    public CleanupChoseCharacterSystem(GameContext context)
    {
        choseCharacterGroup = context.GetGroup(GameMatcher.ChoseCharacter);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in choseCharacterGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }

}