using Entitas;
using SemoGames.PTG.Configurations;
using SemoGames.PTG.Enemy;
using SemoGames.PTG.GameInput;
using SemoGames.PTG.Position;
using System;
using SemoGames.PTG.Battle;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;

    [SerializeField] private CharacterConfiguration characterConfiguration;

    private Systems systems;

    // Use this for initialization
    void Start()
    {
        InitConfigs();

        Contexts pools = Contexts.sharedInstance;
        pools.SetAllContexts();

        systems = CreateSystems(pools.game);
        systems.Initialize();
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
    }

    // Update is called once per frame
    void Update()
    {
        systems.Execute();
        systems.Cleanup();
    }

    private Systems CreateSystems(GameContext context)
    {
        return new Feature("Systems")
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
            .Add(new AttackCooldownSystem(context))
            .Add(new ActTimeSystem(context))
            .Add(new ReadyToActSystem(context));
    }
}