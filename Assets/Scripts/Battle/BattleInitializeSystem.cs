using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Battle
{
    public class BattleInitializeSystem : IInitializeSystem
    {
        private GameContext context;

        public BattleInitializeSystem(GameContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            CreatePlayerEntities();
            CreateEnemyEntities();
        }

        private void CreatePlayerEntities()
        {
            GameObject[] playerCharacters = GameObject.FindGameObjectsWithTag(Tags.Player);

            foreach (GameObject playerCharacter in playerCharacters)
            {
                GameEntity entity = context.CreateEntity();
                entity.isPlayer = true;
                entity.AddView(playerCharacter);
                entity.AddPosition(playerCharacter.transform.position);
                entity.AddHealth(100);
                entity.AddAttack(10);
                entity.AddDefense(5);
            }
        }

        private void CreateEnemyEntities()
        {
            GameObject[] enemyCharacters = GameObject.FindGameObjectsWithTag(Tags.Enemy);

            foreach (GameObject enemyCharacter in enemyCharacters)
            {
                GameEntity entity = context.CreateEntity();
                entity.isEnemy = true;
                entity.AddView(enemyCharacter);
                entity.AddPosition(enemyCharacter.transform.position);
                entity.AddHealth(10);
                entity.AddAttack(10);
                entity.AddDefense(5);
            }
        }
    }
}