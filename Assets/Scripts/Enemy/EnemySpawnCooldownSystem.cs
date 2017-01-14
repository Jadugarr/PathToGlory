using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Enemy
{
    public class EnemySpawnCooldownSystem : IExecuteSystem, ISetPool
    {
        Pool enemyPool;
        Group cooldowns;

        public void Execute()
        {
            if (cooldowns.count > 0)
            {
                Entity cooldownEntity = cooldowns.GetSingleEntity();

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

        public void SetPool(Pool pool)
        {
            enemyPool = pool;
            cooldowns = enemyPool.GetGroup(CoreMatcher.EnemySpawnCooldown);
        }
    }
}
