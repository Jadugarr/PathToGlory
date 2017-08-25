using Entitas;
using System;
using System.Collections.Generic;
using Configurations;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;

    [SerializeField] private CharacterConfiguration characterConfiguration;

    private static GameController controller;

    private Systems currentSystems;
    private Systems universalSystems;
    private Dictionary<GameState, Systems> stateSystemMap;

    private void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        InitConfigs();
        Contexts pools = Contexts.sharedInstance;

        CreateStateSystemMap(pools.game);
        CreateUniversalSystems(pools.game);
    }

    public void ChangeState(GameState state)
    {
        if (currentSystems != null)
        {
            currentSystems.ClearReactiveSystems();
            currentSystems.DeactivateReactiveSystems();
            currentSystems.TearDown();
        }

        currentSystems = stateSystemMap[state];
        currentSystems.Initialize();
        currentSystems.ActivateReactiveSystems();
    }

    private void CreateUniversalSystems(GameContext context)
    {
        universalSystems = new Feature("UniversalSystems")
            //Scene
            .Add(new CleanupChangeSceneSystem(context))
            .Add(new ChangeSceneSystem(context))
            .Add(new ChangedToMainMenuSceneSystem(context))
            //Game State
            .Add(new ChangeGameStateSystem(context, this))
            .Add(new InitializeGameStateSystem())
            //UI
            .Add(new DisplayUISystem(context))
            .Add(new CleanupDisplayUISystem(context));
        universalSystems.Initialize();
    }

    private void CreateStateSystemMap(GameContext context)
    {
        stateSystemMap = new Dictionary<GameState, Systems>();
        CreateMainMenuSystems(context);
        CreateBattleSystems(context);
    }

    private void CreateMainMenuSystems(GameContext context)
    {
        stateSystemMap.Add(GameState.MainMenu, new Feature("MainMenuSystems")
            .Add(new InitializeMainMenuSystem()));
    }

    private void CreateBattleSystems(GameContext context)
    {
        stateSystemMap.Add(GameState.Battle, new Feature("BattleSystems")
            //Initialize
            .Add(new InitializeBattleSystem(context))
            //Input
            .Add(new InputSystem(context))
            .Add(new ProcessEnemySpawnInputSystem(context))
            //Enemy
            .Add(new EnemySpawnCooldownSystem(context))
            //Position
            .Add(new RenderPositionSystem(context))
            //Battle
            .Add(new AttackCharacterSystem(context))
            .Add(new CharacterDeathSystem(context))
            .Add(new ActTimeSystem(context))
            .Add(new ReadyToActSystem(context))
            .Add(new CleanupAttackCharacterSystem(context))
            .Add(new TeardownCharacterSystem(context))
            .Add(new TeardownReadyToActSystem(context)));
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSystems != null)
        {
            currentSystems.Execute();
        }

        universalSystems.Execute();

        if (currentSystems != null)
        {
            currentSystems.Cleanup();
        }

        universalSystems.Cleanup();
    }
}