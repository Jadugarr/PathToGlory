using System;

public interface IActionPropertyAdder
{
    // Maybe replace the "string" for an actual error code to react to?
    void Execute(GameContext context, GameEntity actionEntity, Action successCallback, Action<string> errorCallback);
    void Cancel();
}