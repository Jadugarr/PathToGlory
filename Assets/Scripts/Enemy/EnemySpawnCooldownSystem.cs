using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Enemy
{
    public class EnemySpawnCooldownSystem : IExecuteSystem
    {
        GameContext enemyPool;
        IGroup<GameEntity> cooldowns;

        public EnemySpawnCooldownSystem(GameContext context)
        {
            enemyPool = context;
            cooldowns = enemyPool.GetGroup(GameMatcher.EnemySpawnCooldown);
        }

        public void Execute()
        {
            if (cooldowns.count > 0)
            {
                GameEntity cooldownEntity = cooldowns.GetSingleEntity();
                
                if (cooldownEntity.enemySpawnCooldown.cooldown > 0)
                {
                    cooldownEntity.ReplaceEnemySpawnCooldown(cooldownEntity.enemySpawnCooldown.cooldown - Time.deltaTime);
                }
                else
                {
                    enemyPool.RemoveEnemySpawnCooldown();
                }
            }
        }
    }
}
