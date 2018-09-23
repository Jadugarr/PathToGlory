﻿using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class EnterBattleStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> sceneLoadedGroup;

    public EnterBattleStateSystem(GameContext context) : base(context)
    {
        this.context = context;
        sceneLoadedGroup = context.GetGroup(GameMatcher.SceneLoaded);
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
        sceneLoadedGroup.OnEntityAdded += OnBattleSceneLoaded;

        GameEntity changeSceneEntity = context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.BattleScene, LoadSceneMode.Additive);
    }

    private void OnBattleSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        sceneLoadedGroup.OnEntityAdded -= OnBattleSceneLoaded;

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
            .Add(new CharacterDeathSystem(context))
            .Add(new TeardownCharacterSystem(context))
            .Add(new TeardownBattleSystem(context))
            .Add(new DisplayBattleResultSystem(context))
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
            .Add(new InitializeAndTeardownWinConditionsSystem(context))
            .Add(new InitializeAndTeardownLoseConditionsSystem(context))
            .Add(new WinConditionControllerSystem(context))
            .Add(new LoseConditionControllerSystem(context))
            .Add(new BattleEndSystem(context));


        GameSystemService.AddSystemMapping(GameState.Battle, battleSystems);
    }
}