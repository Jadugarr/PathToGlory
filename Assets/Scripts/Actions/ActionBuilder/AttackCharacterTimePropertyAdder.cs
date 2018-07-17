using System;
using Promises;

public class AttackCharacterTimePropertyAdder : IActionPropertyAdder
{
    public void Execute(GameContext context, GameEntity actionEntity, Action successCallback,
        Action<string> errorCallback)
    {
        if (actionEntity.hasExecutionTime)
        {
            actionEntity.ReplaceExecutionTime(3f, 3f);
        }
        else
        {
            actionEntity.AddExecutionTime(3f, 3f);
        }
        successCallback();
    }

    public void Cancel()
    {
    }
}