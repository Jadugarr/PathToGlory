﻿using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class EnterBattleStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> sceneChangedGroup;

    public EnterBattleStateSystem(GameContext context) : base(context)
    {
        this.context = context;
        sceneChangedGroup = context.GetGroup(GameMatcher.SceneChanged);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        sceneChangedGroup.OnEntityAdded += OnBattleSceneLoaded;

        //InputConfiguration.ChangeActiveGameStateInputMap(GameState.Battle);
        GameEntity changeSceneEntity = context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.BattleScene, LoadSceneMode.Additive);
    }

    private void OnBattleSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        sceneChangedGroup.OnEntityAdded -= OnBattleSceneLoaded;

        if (!GameSystemService.HasSystemMapping(GameState.Battle))
        {
            CreateBattleSystems();
        }

        Systems battleSystems = GameSystemService.GetSystemMapping(GameState.Battle);
        battleSystems.ActivateReactiveSystems();
        battleSystems.Initialize();
        GameSystemService.AddActiveSystems(battleSystems);
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.Waiting);
    }

    private void CreateBattleSystems()
    {
        Systems battleSystems = new Feature("BattleStateSystems")
            .Add(new InitializeBattleSystem(context))
            .Add(new InitializeATBSystem(context))
            //Enemy
            .Add(new EnemySpawnCooldownSystem(context))
            //Battle
            .Add(new AttackCharacterSystem(context))
            .Add(new CharacterDeathSystem(context))
            .Add(new CleanupAttackCharacterSystem(context))
            .Add(new TeardownCharacterSystem(context))
            .Add(new TeardownBattleSystem(context))
            .Add(new CleanupDeadCharacterSystem(context))
            //Actions
            .Add(new ExecutePlayerChooseActionSystem(context))
            .Add(new ExecuteEnemyChooseActionSystem(context))
            .Add(new ExecutePlayerAttackActionSystem(context))
            .Add(new ExecuteDefenseActionSystem(context))
            .Add(new ReleaseDefenseActionSystem(context))
            .Add(new CleanupChoseActionSystem(context))
            .Add(new CleanupChoseCharacterSystem(context))
            .Add(new ActionFinishedSystem(context))
            //WinConditions
            .Add(new CheckKillEnemiesConditionSystem(context));

        GameSystemService.AddSystemMapping(GameState.Battle, battleSystems);
    }
}