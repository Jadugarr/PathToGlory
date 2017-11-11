using System;

public class DefenseActionTimePropertyAdder : IActionPropertyAdder
{
    public void Execute(GameContext context, GameEntity actionEntity, Action successCallback, Action<string> errorCallback)
    {
        if (actionEntity.hasExecutionTime)
        {
            actionEntity.ReplaceExecutionTime(0.1f, 0.1f);
        }
        else
        {
            actionEntity.AddExecutionTime(0.1f, 0.1f);
        }
        successCallback();
    }

    public void Cancel()
    {
        // Don't think I have to do anything here, since I just add components to an entity and that's it
    }
}