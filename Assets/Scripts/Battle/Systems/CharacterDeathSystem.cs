using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterDeathSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> actionEntityGroup;

    public CharacterDeathSystem(IContext<GameEntity> context) : base(context)
    {
        actionEntityGroup = context.GetGroup(GameMatcher.BattleAction);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Health, GameMatcher.View));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth && entity.health.Health <= 0;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameObject.Destroy(gameEntity.view.View);

            foreach (GameEntity actionEntity in actionEntityGroup.GetEntities())
            {
                if (actionEntity.battleAction.EntityId == gameEntity.id.Id)
                {
                    actionEntity.Destroy();
                }
            }

            Debug.Log("Enemy died!");

            gameEntity.Destroy();
        }
    }
}