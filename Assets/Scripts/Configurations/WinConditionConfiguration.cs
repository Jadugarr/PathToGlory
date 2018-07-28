using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Battle.Enums;

public static class WinConditionConfiguration
{
    private static Dictionary<WinCondition, ISystem> winConditionSystemMap = new Dictionary<WinCondition, ISystem>();
}