using Entitas;
using Entitas.CodeGenerator.Api;


[Game]
[Unique]
public class AttackCooldownComponent : IComponent
{
    public float Cooldown;
}