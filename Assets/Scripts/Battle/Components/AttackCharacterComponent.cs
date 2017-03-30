using Entitas;

namespace SemoGames.PTG.Battle
{
    [Game]
    public class AttackCharacterComponent : IComponent
    {
        public GameEntity AttackerEntity;
        public GameEntity DefenderEntity;
    }
}
