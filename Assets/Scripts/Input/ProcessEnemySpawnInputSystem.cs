using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace SemoGames.PTG.GameInput
{
    public class ProcessEnemySpawnInputSystem : ISetPools, IReactiveSystem
    {
        Pools pools;

        public TriggerOnEvent trigger { get { return CoreMatcher.EnemySpawnInput.OnEntityAdded(); } }
        
        public void Execute(List<Entity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                //Create a better system to load assets/prefabs --> ScriptableObjects?
                GameObject newEnemy = GameObject.Instantiate((GameObject)Resources.Load("EnemyTemplate"));

                pools.core.CreateEntity()
                .IsEnemy(true)
                .AddView(newEnemy)
                .AddPosition(newEnemy.transform.position);
                Debug.Log("Enemy spawned");
            }

            pools.core.SetEnemySpawnCooldown(5f);
        }

        public void SetPools(Pools pools)
        {
            this.pools = pools;
        }
    }
}
