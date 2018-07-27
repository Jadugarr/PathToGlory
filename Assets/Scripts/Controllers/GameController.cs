using Entitas;
using System;
using System.Collections.Generic;
using Configurations;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;
    [SerializeField] private CharacterConfiguration characterConfiguration;

    private static GameController controller;

    private void Awake()
    {
        if (controller == null)
        {
            //DontDestroyOnLoad(gameObject);
            controller = this;

            Contexts contexts = Contexts.sharedInstance;
            foreach (var context in contexts.allContexts)
            {
                context.OnEntityCreated += OnEntityCreated;
            }
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

        CreateUniversalSystems(pools.game);
    }

    // add an id to every entity as it's created
    private void OnEntityCreated(IContext context, IEntity entity)
    {
        (entity as GameEntity).AddId(entity.creationIndex);
    }

    private void CreateUniversalSystems(GameContext context)
    {
        Systems universalSystems = new Feature("UniversalSystems")
            //Promises
            .Add(new InitPromisesSystem())
            //Scene
            .Add(new EnterBattleStateSystem(context))
            .Add(new ExitBattleStateSystem(context))
            .Add(new EnterMainMenuStateSystem(context))
            .Add(new ExitMainMenuStateSystem(context))
            .Add(new ChangeSceneSystem(context))
            .Add(new UnloadSceneSystem(context))
            .Add(new CleanupSceneChangedSystem(context))
            .Add(new CleanupUnloadSceneSystem(context))
            //Game State
            .Add(new ChangeGameStateSystem(context))
            .Add(new InitializeGameStateSystem())
            //Sub State
            .Add(new ChangeSubStateSystem(context))
            .Add(new EnterPausedSubStateSystem(context))
            .Add(new ExitPausedSubStateSystem(context))
            .Add(new EnterWaitingSubStateSystem(context))
            .Add(new ExitWaitingSubStateSystem(context))
            .Add(new EnterChoosingSubStateSystem(context))
            //UI
            .Add(new DisplayUISystem(context))
            .Add(new HideUiSystem(context))
            //Position
            .Add(new RenderPositionSystem(context))
            //Input
            .Add(new InputSystem(context))
            .Add(new ProcessPauseInputSystem(context))
            .Add(new ProcessUnpauseInputSystem(context));
        universalSystems.Initialize();

        GameSystemService.AddActiveSystems(universalSystems);
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
    }

    // Update is called once per frame
    void Update()
    {
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Execute();
        }

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Cleanup();
        }

        GameSystemService.RefreshActiveSystems();
    }
}