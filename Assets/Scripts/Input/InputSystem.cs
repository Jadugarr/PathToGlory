using Entitas;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class InputSystem : IExecuteSystem, ISetPools, ICleanupSystem
    {
        private Pool corePool;
        private Group enemySpawnInput;

        public void Execute()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0 && !corePool.hasEnemySpawnCooldown)
            {
                corePool.CreateEntity()
                    .IsEnemySpawnInput(true);
            }
        }

        public void SetPools(Pools pools)
        {
            corePool = pools.core;
            enemySpawnInput = corePool.GetGroup(CoreMatcher.EnemySpawnInput);
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
