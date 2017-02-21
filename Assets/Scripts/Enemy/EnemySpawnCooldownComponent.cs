using Entitas;
using Entitas.CodeGenerator.Api;

namespace SemoGames.PTG.Enemy
{
    [Game]
    [Unique]
    public class EnemySpawnCooldownComponent : IComponent
    {
        public float cooldown;
    }
}
