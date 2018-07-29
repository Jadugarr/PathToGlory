using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Battle.Enums;

public static class WinConditionConfiguration
{
    private static Dictionary<WinCondition, Type> winConditionSystemMap = new Dictionary<WinCondition, Type>
    {
        {WinCondition.KillEnemies, typeof(CheckKillEnemiesConditionSystem) }
    };

    public static ISystem GetSystemForWinCondition(WinCondition winCondition, GameContext context)
    {
        Type winConditionCheckClassType;

        if (winConditionSystemMap.TryGetValue(winCondition, out winConditionCheckClassType))
        {
            ISystem system = (ISystem)Activator.CreateInstance(winConditionCheckClassType, context);

            return system;
        }

        return null;
    }
}