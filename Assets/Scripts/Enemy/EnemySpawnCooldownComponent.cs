using Entitas;
using Entitas.CodeGenerator.Api;

[Game]
[Unique]
public class EnemySpawnCooldownComponent : IComponent
{
    public float cooldown;
}