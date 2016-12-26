using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class ProcessEnemySpawnInputSystem : ISetPools, IReactiveSystem
    {
        Pools pools;

        public TriggerOnEvent trigger { get { return InputMatcher.EnemySpawnInput.OnEntityAdded(); } }
        
        public void Execute(List<Entity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                pools.enemy.CreateEntity()
                .IsEnemy(true);
                Debug.Log("Enemy spawned");
            }

            pools.enemy.SetEnemySpawnCooldown(5f);
        }

        public void SetPools(Pools pools)
        {
            this.pools = pools;
        }
    }
}
