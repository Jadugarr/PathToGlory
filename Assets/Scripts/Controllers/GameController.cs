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
    private GameState currentGameState;
    private Dictionary<GameState, Systems> stateSystemMap;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        InitConfigs();
        Contexts pools = Contexts.sharedInstance;

        SetInitialState();
        CreateStateSystemMap(pools.game);

        currentSystems = stateSystemMap[currentGameState];
    }

    private void SetInitialState()
    {
        currentGameState = GameState.MainMenu;
    }

    private void CreateStateSystemMap(GameContext context)
    {
        stateSystemMap = new Dictionary<GameState, Systems>();
        CreateMainMenuSystems(context);
        CreateBattleSystems(context);
    }

    private void CreateMainMenuSystems(GameContext context)
    {
        stateSystemMap.Add(GameState.MainMenu, new Systems());
        stateSystemMap[GameState.MainMenu].Initialize();
    }

    private void CreateBattleSystems(GameContext context)
    {
        stateSystemMap.Add(GameState.Battle, new Systems()
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

        stateSystemMap[GameState.Battle].Initialize();
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
        currentSystems.Cleanup();
    }
}