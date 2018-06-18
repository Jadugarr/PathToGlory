﻿using System;
using Promises;

public interface IActionPropertyAdder
{
    // Maybe replace the "string" for an actual error code to react to?
    Deferred<GameEntity> Execute(GameContext context, GameEntity actionEntity, Action successCallback, Action<string> errorCallback);
    void Cancel();
}