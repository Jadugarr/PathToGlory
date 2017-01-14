using Entitas;
using SemoGames.PTG.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class ProcessEnemySpawnInputSystem : ReactiveSystem
    {
        Context pool;

        public ProcessEnemySpawnInputSystem(Context context) : base(context) {
        this.pool = context;
    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(CoreMatcher.EnemySpawnInput);
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }
        
    protected override void Execute(List<Entity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(GameConfigurations.CharacterConfiguration.EnemyTemplate);

            pool.CreateEntity()
            .IsEnemy(true)
            .AddView(newEnemy)
            .AddPosition(newEnemy.transform.position);
            Debug.Log("Enemy spawned");
        }

        pool.SetEnemySpawnCooldown(5f);
    }

        
    }
}
