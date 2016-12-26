using Entitas;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class InputSystem : IExecuteSystem, ISetPools, ICleanupSystem
    {
        private Pool inputPool;
        private Group enemySpawnInput;

        public void Execute()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0)
            {
                inputPool.CreateEntity()
                    .IsEnemySpawnInput(true);
            }
        }

        public void SetPools(Pools pools)
        {
            inputPool = pools.input;
            enemySpawnInput = inputPool.GetGroup(InputMatcher.EnemySpawnInput);
        }

        public void Cleanup()
        {
            Entity[] entities = enemySpawnInput.GetEntities();

            for (int i = 0; i < entities.Length; i++)
            {
                inputPool.DestroyEntity(entities[i]);
            }
        }
    }
}
