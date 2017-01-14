using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Enemy
{
    public class EnemySpawnCooldownSystem : IExecuteSystem
    {
        Context enemyPool;
        Group cooldowns;

        public EnemySpawnCooldownSystem(Context context)
        {
            enemyPool = context;
            cooldowns = enemyPool.GetGroup(CoreMatcher.EnemySpawnCooldown);
        }

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
    }
}
