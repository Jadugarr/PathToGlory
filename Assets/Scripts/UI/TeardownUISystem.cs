using Entitas;
using UnityEngine;

public class TeardownUISystem : ITearDownSystem
{
    private IGroup<GameEntity> uiEntities;

    public TeardownUISystem(GameContext context)
    {
        uiEntities = context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.UI, GameMatcher.View));
    }

    public void TearDown()
    {
        foreach (GameEntity uiEntity in uiEntities.GetEntities())
        {
            GameObject.Destroy(uiEntity.view.View);
            uiEntity.Destroy();
        }
    }
}