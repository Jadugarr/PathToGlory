using System;
using Entitas;

public class TeardownReadyToChooseActionSystem : ITearDownSystem
{
    private IGroup<GameEntity> readyToAct;

    public TeardownReadyToChooseActionSystem(GameContext context)
    {
        readyToAct = context.GetGroup(GameMatcher.ReadyToChooseAction);
    }

    public void TearDown()
    {
        foreach (GameEntity gameEntity in readyToAct.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}