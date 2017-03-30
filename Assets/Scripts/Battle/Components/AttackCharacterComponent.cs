using Entitas;

[Game]
public class AttackCharacterComponent : IComponent
{
    public GameEntity AttackerEntity;
    public GameEntity DefenderEntity;
}