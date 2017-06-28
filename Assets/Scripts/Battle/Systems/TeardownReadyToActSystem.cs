using System;
using Entitas;

public class TeardownReadyToActSystem : ITearDownSystem
{
    private IGroup<GameEntity> readyToAct;

    public TeardownReadyToActSystem(GameContext context)
    {
        readyToAct = context.GetGroup(GameMatcher.ReadyToAct);
    }

    public void TearDown()
    {
        foreach (GameEntity gameEntity in readyToAct.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}