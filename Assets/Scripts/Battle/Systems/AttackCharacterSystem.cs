using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Battle
{
    public class AttackCharacterSystem : ReactiveSystem<GameEntity>
    {
        private GameContext context;
        private IGroup<GameEntity> readyToActEntities;

        public AttackCharacterSystem(GameContext context) : base(context)
        {
            this.context = context;
            readyToActEntities = context.GetGroup(GameMatcher.ReadyToAct);
        }

        protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AttackCharacter);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity gameEntity in entities)
            {
                gameEntity.attackCharacter.DefenderEntity.ReplaceHealth(
                    gameEntity.attackCharacter.DefenderEntity.health.Health -
                    Math.Max(0,
                        gameEntity.attackCharacter.AttackerEntity.attack.AttackValue -
                        gameEntity.attackCharacter.DefenderEntity.defense.DefenseValue));

                Debug.Log("Enemy attacked! Remaining health: " + gameEntity.attackCharacter.DefenderEntity.health.Health);

                if (gameEntity.attackCharacter.DefenderEntity.health.Health <= 0)
                {
                    context.CreateEntity().AddDeath(gameEntity.attackCharacter.DefenderEntity);
                }

                foreach (GameEntity entity in readyToActEntities.GetEntities())
                {
                    if (entity.readyToAct.EntityReadyToAct == gameEntity.attackCharacter.AttackerEntity)
                    {
                        gameEntity.attackCharacter.AttackerEntity.ReplaceTimeUntilAction(10f);
                        context.DestroyEntity(entity);
                    }
                }

                context.DestroyEntity(gameEntity);
            }
        }
    }
}