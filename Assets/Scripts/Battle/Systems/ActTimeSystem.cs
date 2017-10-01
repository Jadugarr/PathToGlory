using Entitas;
using UnityEngine;

public class ActTimeSystem : IExecuteSystem
{
    private GameContext context;
    private IGroup<GameEntity> actEntities;
    private IGroup<GameEntity> readyToActEntites;

    public ActTimeSystem(GameContext context)
    {
        this.context = context;
        actEntities = context.GetGroup(GameMatcher.AllOf(GameMatcher.TimeUntilChooseAction, GameMatcher.Battle));
        readyToActEntites = context.GetGroup(GameMatcher.ReadyToAct);
    }

    public void Execute()
    {
        if (readyToActEntites.count == 0)
        {
            GameEntity[] entities = actEntities.GetEntities();

            foreach (GameEntity gameEntity in entities)
            {
                gameEntity.ReplaceTimeUntilChooseAction(gameEntity.timeUntilChooseAction.RemainingTime - Time.deltaTime * gameEntity.speed.SpeedValue, gameEntity.timeUntilChooseAction.TotalTime);

                if (gameEntity.timeUntilChooseAction.RemainingTime <= 0f)
                {
                    GameEntity readyToAct = context.CreateEntity();
                    readyToAct.AddReadyToAct(gameEntity.id.Id);
                }
            }
        }
    }
}