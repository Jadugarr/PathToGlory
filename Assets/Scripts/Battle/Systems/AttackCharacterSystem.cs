using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AttackCharacterSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> readyToActEntities;

    public AttackCharacterSystem(GameContext context) : base(context)
    {
        this.context = context;
        readyToActEntities = context.GetGroup(GameMatcher.ReadyToAct);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
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
            GameEntity attacker = context.GetEntityWithId(gameEntity.attackCharacter.AttackerEntityId);
            GameEntity defender = context.GetEntityWithId(gameEntity.attackCharacter.DefenderEntityId); ;
            defender.ReplaceHealth(
                defender.health.Health -
                Math.Max(0,
                    attacker.attack.AttackValue -
                    defender.defense.DefenseValue));

            Debug.Log("Enemy attacked! Remaining health: " + defender.health.Health);

            if (defender.health.Health <= 0)
            {
                context.CreateEntity().AddDeath(defender);
            }

            foreach (GameEntity entity in readyToActEntities.GetEntities())
            {
                GameEntity readyToActEntity = context.GetEntityWithId(entity.readyToAct.EntityReadyToActId);
                if (readyToActEntity == attacker)
                {
                    attacker.ReplaceTimeUntilChooseAction(10f, 10f);
                    entity.Destroy();
                }
            }
        }
    }
}