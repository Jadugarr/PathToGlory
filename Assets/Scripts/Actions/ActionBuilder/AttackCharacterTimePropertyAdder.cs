using System;
using Promises;

public class AttackCharacterTimePropertyAdder : IActionPropertyAdder
{
    private Deferred<GameEntity> promise;

    public Deferred<GameEntity> Execute(GameContext context, GameEntity actionEntity, Action successCallback,
        Action<string> errorCallback)
    {
        promise = new Deferred<GameEntity>();
        if (actionEntity.hasExecutionTime)
        {
            actionEntity.ReplaceExecutionTime(3f, 3f);
        }
        else
        {
            actionEntity.AddExecutionTime(3f, 3f);
        }
        successCallback();
        return promise;
    }

    public void Cancel()
    {
    }
}