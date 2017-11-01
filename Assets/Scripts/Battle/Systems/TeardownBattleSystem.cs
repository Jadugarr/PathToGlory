using Entitas;

public class TeardownBattleSystem : ITearDownSystem
{
    private IGroup<GameEntity> battleEntities;

    public TeardownBattleSystem(GameContext context)
    {
        battleEntities = context.GetGroup(Matcher<GameEntity>.AnyOf(GameMatcher.BattleAction, GameMatcher.ExecuteAction));
    }

    public void TearDown()
    {
        foreach (GameEntity gameEntity in battleEntities.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}