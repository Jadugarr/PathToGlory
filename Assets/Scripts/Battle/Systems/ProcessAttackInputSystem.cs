using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProcessAttackInputSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> playerEntities;
    private IGroup<GameEntity> enemyEntities;
    private IGroup<GameEntity> readyToActEntities;

    public ProcessAttackInputSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext)context;
        playerEntities = context.GetGroup(GameMatcher.Player);
        enemyEntities = context.GetGroup(GameMatcher.Enemy);
        readyToActEntities = context.GetGroup(GameMatcher.ReadyToAct);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AttackInput);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] players = playerEntities.GetEntities();
        GameEntity[] enemies = enemyEntities.GetEntities();

        if (players.Length > 0)
        {
            if (IsPlayerReadyToAct())
            {
                if (enemies.Length > 0)
                {
                    GameEntity attackEntity = context.CreateEntity();
                    attackEntity.AddAttackCharacter(players[0].id.Id, enemies[Random.Range(0, enemies.Length)].id.Id);
                }
                else
                {
                    Debug.LogError("There are no enemies in the pool!");
                }
            }
        }
        else
        {
            Debug.LogError("Wtf? There's no player entity in the pool.");
        }
    }
    
    private bool IsPlayerReadyToAct()
    {
        GameEntity[] entities = readyToActEntities.GetEntities();

        foreach (GameEntity gameEntity in entities)
        {
            GameEntity readyToActEntity = context.GetEntityWithId(gameEntity.readyToAct.EntityReadyToActId);

            if (readyToActEntity.isPlayer)
            {
                return true;
            }
        }

        return false;
    }
}