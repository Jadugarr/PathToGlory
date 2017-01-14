using Entitas;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class InputSystem : IExecuteSystem, ICleanupSystem
    {
        private Context corePool;
        private Group enemySpawnInput;

        public InputSystem(Context context)
        {
            corePool = context;
            enemySpawnInput = corePool.GetGroup(CoreMatcher.EnemySpawnInput);
        }

        public void Execute()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0 && !corePool.hasEnemySpawnCooldown)
            {
                corePool.CreateEntity()
                    .IsEnemySpawnInput(true);
            }
        }

        public void Cleanup()
        {
            Entity[] entities = enemySpawnInput.GetEntities();

            for (int i = 0; i < entities.Length; i++)
            {
                corePool.DestroyEntity(entities[i]);
            }
        }
    }
}
