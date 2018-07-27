using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class EnterMainMenuStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> sceneChangedGroup;

    public EnterMainMenuStateSystem(GameContext context) : base(context)
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
        return entity.gameState.CurrentGameState == GameState.MainMenu;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //InputConfiguration.ChangeActiveGameStateInputMap(GameState.MainMenu);
        GameEntity changeSceneEntity = context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.MainMenuScene, LoadSceneMode.Additive);
        sceneChangedGroup.OnEntityAdded += OnMainMenuSceneLoaded;
    }

    private void OnMainMenuSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        sceneChangedGroup.OnEntityAdded -= OnMainMenuSceneLoaded;

        if (!GameSystemService.HasSystemMapping(GameState.MainMenu))
        {
            CreateMainMenuSystems();
        }

        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameState.MainMenu);
        mainMenuSystems.ActivateReactiveSystems();
        mainMenuSystems.Initialize();

        GameSystemService.AddActiveSystems(mainMenuSystems);
    }

    private void CreateMainMenuSystems()
    {
        Systems mainMenuSystems = new Feature("MainMenuSystems");
        mainMenuSystems.Add(new InitializeMainMenuSystem());

        GameSystemService.AddSystemMapping(GameState.MainMenu, mainMenuSystems);
    }
}