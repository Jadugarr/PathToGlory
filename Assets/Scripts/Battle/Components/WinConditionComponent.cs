using Entitas;
using Entitas.Scripts.Battle.Enums;

[Game]
public class WinConditionComponent : IComponent
{
    public WinConditionModifier WinConditionModifier;
    public WinConditionState[] WinConditions;
}

public struct WinConditionState
{
    public bool IsFulfilled;
    public WinCondition WinCondition;
}