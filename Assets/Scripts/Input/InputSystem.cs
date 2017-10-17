using Entitas;
using UnityEngine;

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
        //CheckAttackInput();
    }

    private void CheckSpawnInput()
    {
        float spawnAxis = UnityEngine.Input.GetAxis("EnemySpawn");

        if (spawnAxis > 0 && !context.hasEnemySpawnCooldown)
        {
            context.CreateEntity()
                .isEnemySpawnInput = true;
        }
    }

    //private void CheckAttackInput()
    //{
    //    float attackAxis = Input.GetAxis("Attack");

    //    if (attackAxis > 0)
    //    {
    //        GameEntity attackInputEntity = context.CreateEntity();
    //        attackInputEntity.isAttackInput = true;
    //    }
    //}

    public void Cleanup()
    {
        GameEntity[] entities = enemySpawnInput.GetEntities();

        for (int i = 0; i < entities.Length; i++)
        {
            entities[i].Destroy();
        }
    }
}