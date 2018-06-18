using System;
using Promises;

public class DefenseActionTimePropertyAdder : IActionPropertyAdder
{
    private Deferred<GameEntity> promise;

    public Deferred<GameEntity> Execute(GameContext context, GameEntity actionEntity, Action successCallback, Action<string> errorCallback)
    {
        promise = new Deferred<GameEntity>();
        if (actionEntity.hasExecutionTime)
        {
            actionEntity.ReplaceExecutionTime(0.1f, 0.1f);
        }
        else
        {
            actionEntity.AddExecutionTime(0.1f, 0.1f);
        }
        successCallback();
        return promise;
    }

    public void Cancel()
    {
        // Don't think I have to do anything here, since I just add components to an entity and that's it
    }
}