using Entitas;
using Entitas.CodeGenerator;

namespace SemoGames.PTG.Enemy
{
    [Enemy]
    [SingleEntity]
    public class EnemySpawnCooldownComponent : IComponent
    {
        public float cooldown;
    }
}
