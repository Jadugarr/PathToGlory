using Entitas;
using Entitas.CodeGenerator;

namespace SemoGames.PTG.Enemy
{
    [Core]
    [SingleEntity]
    public class EnemySpawnCooldownComponent : IComponent
    {
        public float cooldown;
    }
}
