using Entitas;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class InputSystem : IExecuteSystem, ICleanupSystem
    {
        private GameContext corePool;
        private IGroup<GameEntity> enemySpawnInput;

        public InputSystem(GameContext context)
        {
            corePool = context;
            enemySpawnInput = corePool.GetGroup(GameMatcher.EnemySpawnInput);
        }

        public void Execute()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0 && !corePool.hasEnemySpawnCooldown)
            {
                corePool.CreateEntity()
                    .isEnemySpawnInput = true;
            }
        }

        public void Cleanup()
        {
            GameEntity[] entities = enemySpawnInput.GetEntities();

            for (int i = 0; i < entities.Length; i++)
            {
                corePool.DestroyEntity(entities[i]);
            }
        }
    }
}
