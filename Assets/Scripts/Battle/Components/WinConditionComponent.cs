using Entitas;
using Entitas.CodeGeneration.Attributes;
using Entitas.Scripts.Battle.Enums;

[Game, Unique]
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