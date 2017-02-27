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
            CheckSpawnInput();
            CheckAttackInput();
        }

        private void CheckSpawnInput()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0 && !corePool.hasEnemySpawnCooldown)
            {
                corePool.CreateEntity()
                    .isEnemySpawnInput = true;
            }
        }

        private void CheckAttackInput()
        {
            float attackAxis = Input.GetAxis("Attack");

            if (attackAxis > 0 && !corePool.hasAttackCooldown)
            {
                GameEntity[] players = corePool.GetEntities(GameMatcher.Player);
                GameEntity[] enemies = corePool.GetEntities(GameMatcher.Enemy);

                if (players.Length > 0)
                {
                    if (enemies.Length > 0)
                    {
                        GameEntity attackEntity = corePool.CreateEntity();
                        attackEntity.AddAttackCharacter(players[0], enemies[Random.Range(0, enemies.Length)]);
                    }
                    else
                    {
                        Debug.LogError("There are no enemies in the pool!");
                    }
                }
                else
                {
                    Debug.LogError("Wtf? There's no player entity in the pool.");
                }

                corePool.SetAttackCooldown(2f);
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
