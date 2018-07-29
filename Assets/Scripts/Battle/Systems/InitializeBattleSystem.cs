﻿using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Battle.Enums;
using UnityEngine;

public class InitializeBattleSystem : IInitializeSystem
{
    private GameContext context;

    public InitializeBattleSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        CreatePlayerEntities();
        CreateEnemyEntities();
        CreateReturnButton();
        CreateWinConditions();
    }

    private void CreateWinConditions()
    {
        context.CreateEntity()
            .AddWinCondition(WinConditionModifier.All,
                new[] {new WinConditionState {IsFulfilled = false, WinCondition = WinCondition.KillEnemies}});
    }

    private void CreateReturnButton()
    {
        context.CreateEntity()
            .AddDisplayUI(AssetTypes.ReturnButton, new ReturnButtonProperties());
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
            entity.AddDefenseStat(5);
            entity.AddSpeed(10);
            entity.AddCharacterBattleState(CharacterBattleState.WaitingToChoose);
            entity.AddBattleActionChoices(new List<BattleActionChoice>
            {
                new BattleActionChoice
                {
                    ActionType = ActionType.AttackCharacter,
                    IsAvailable = true
                },
                new BattleActionChoice
                {
                    ActionType = ActionType.Defend,
                    IsAvailable = true
                }
            });
            SpriteRenderer spriteRenderer = playerCharacter.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                entity.AddBattleImage(spriteRenderer.sprite);
            }

            entity.isBattle = true;

            GameEntity actionEntity = context.CreateEntity();
            actionEntity.AddExecutionTime(10f, 10f);
            actionEntity.AddBattleAction(entity.id.Id, ActionType.ChooseAction, ActionATBType.Waiting);
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
            entity.AddDefenseStat(5);
            entity.AddSpeed(5);
            entity.AddCharacterBattleState(CharacterBattleState.WaitingToChoose);
            entity.AddBattleActionChoices(new List<BattleActionChoice>
            {
                new BattleActionChoice
                {
                    ActionType = ActionType.AttackCharacter,
                    IsAvailable = true
                },
                new BattleActionChoice
                {
                    ActionType = ActionType.Defend,
                    IsAvailable = true
                }
            });
            SpriteRenderer spriteRenderer = enemyCharacter.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                entity.AddBattleImage(spriteRenderer.sprite);
            }

            entity.isBattle = true;

            GameEntity actionEntity = context.CreateEntity();
            actionEntity.AddExecutionTime(10f, 10f);
            actionEntity.AddBattleAction(entity.id.Id, ActionType.ChooseAction, ActionATBType.Waiting);
        }
    }
}