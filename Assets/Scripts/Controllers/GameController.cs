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

    private Systems currentSystems;
    private Systems universalSystems;
    private GameState currentGameState;
    private Dictionary<GameState, Systems> stateSystemMap;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        InitConfigs();
        Contexts pools = Contexts.sharedInstance;

        CreateStateSystemMap(pools.game);
        CreateUniversalSystems(pools.game);

        SetInitialState();
    }

    private void ChangeState(GameState state)
    {
        if (currentSystems != null)
        {
            currentSystems.ClearReactiveSystems();
            currentSystems.DeactivateReactiveSystems();
        }

        currentGameState = state;

        currentSystems = stateSystemMap[state];
        currentSystems.Initialize();
        currentSystems.ActivateReactiveSystems();
    }

    private void SetInitialState()
    {
        ChangeState(GameState.MainMenu);
    }

    private void CreateUniversalSystems(GameContext context)
    {
        universalSystems = new Feature("UniversalSystems")
            .Add(new ChangeSceneSystem(context));
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
        stateSystemMap.Add(GameState.MainMenu, new Feature("MainMenuSystems"));
    }

    private void CreateBattleSystems(GameContext context)
    {
        stateSystemMap.Add(GameState.Battle, new Feature("BattleSystems")
            //Initialize
            .Add(new BattleInitializeSystem(context))
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
            .Add(new ReadyToActSystem(context)));
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
    }

    // Update is called once per frame
    void Update()
    {
        currentSystems.Execute();
        universalSystems.Execute();
        currentSystems.Cleanup();
        universalSystems.Cleanup();
    }
}