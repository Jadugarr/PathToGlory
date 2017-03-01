using Entitas;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class InputSystem : IExecuteSystem, ICleanupSystem
    {
        private GameContext context;
        private IGroup<GameEntity> enemySpawnInput;
        private IGroup<GameEntity> readyToActEntities;

        public InputSystem(GameContext context)
        {
            this.context = context;
            enemySpawnInput = this.context.GetGroup(GameMatcher.EnemySpawnInput);
            readyToActEntities = this.context.GetGroup(GameMatcher.ReadyToAct);
        }

        public void Execute()
        {
            CheckSpawnInput();
            CheckAttackInput();
        }

        private void CheckSpawnInput()
        {
            float spawnAxis = Input.GetAxis("EnemySpawn");

            if (spawnAxis > 0 && !context.hasEnemySpawnCooldown)
            {
                context.CreateEntity()
                    .isEnemySpawnInput = true;
            }
        }

        private void CheckAttackInput()
        {
            if (IsPlayerReadyToAct())
            {
                float attackAxis = Input.GetAxis("Attack");

                if (attackAxis > 0 && !context.hasAttackCooldown)
                {
                    GameEntity[] players = context.GetEntities(GameMatcher.Player);
                    GameEntity[] enemies = context.GetEntities(GameMatcher.Enemy);

                    if (players.Length > 0)
                    {
                        if (enemies.Length > 0)
                        {
                            GameEntity attackEntity = context.CreateEntity();
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

                    context.SetAttackCooldown(2f);
                }
            }
        }

        private bool IsPlayerReadyToAct()
        {
            GameEntity[] entities = readyToActEntities.GetEntities();

            foreach (GameEntity gameEntity in entities)
            {
                if (gameEntity.readyToAct.EntityReadyToAct.isPlayer)
                {
                    return true;
                }
            }

            return false;
        }

        public void Cleanup()
        {
            GameEntity[] entities = enemySpawnInput.GetEntities();

            for (int i = 0; i < entities.Length; i++)
            {
                context.DestroyEntity(entities[i]);
            }
        }
    }
}