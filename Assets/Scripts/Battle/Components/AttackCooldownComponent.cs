using Entitas;
using Entitas.CodeGenerator.Api;

namespace SemoGames.PTG.Battle
{
    [Game]
    [Unique]
    public class AttackCooldownComponent : IComponent
    {
        public float Cooldown;
    }
}