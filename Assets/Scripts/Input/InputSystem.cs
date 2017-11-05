using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem, ICleanupSystem
{
    private GameContext context;
    private IGroup<GameEntity> enemySpawnInput;

    public InputSystem(GameContext context)
    {
        this.context = context;
        enemySpawnInput = this.context.GetGroup(GameMatcher.EnemySpawnInput);
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

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in enemySpawnInput)
        {
            gameEntity.Destroy();
        }
    }
}