using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ProcessEnemySpawnInputSystem : GameReactiveSystem
{
    GameContext context;

    public ProcessEnemySpawnInputSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> pool)
    {
        return pool.CreateCollector(GameMatcher.EnemySpawnInput);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(GameConfigurations.CharacterConfiguration.EnemyTemplate);

            GameEntity entity = context.CreateEntity();
            newEnemy.Link(entity);
            entity.isEnemy = true;
            entity.AddView(newEnemy);
            entity.AddPosition(newEnemy.transform.position);
            Debug.Log("Enemy spawned");
        }

        context.SetEnemySpawnCooldown(5f);
    }
}