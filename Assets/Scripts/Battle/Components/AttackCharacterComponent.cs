using Entitas;

[Game]
public class AttackCharacterComponent : IComponent
{
    public int AttackerEntityId;
    public int DefenderEntityId;
}