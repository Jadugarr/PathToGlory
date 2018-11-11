using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class ChangeSubStateComponent : IComponent
{
    public SubState NewSubState;
}