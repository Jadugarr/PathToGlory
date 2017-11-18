using System.Collections.Generic;
using Entitas;

public class ExitMainMenuStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitMainMenuStateSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState == GameState.MainMenu;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameState.MainMenu);
        if (mainMenuSystems != null)
        {
            mainMenuSystems.ClearReactiveSystems();
            mainMenuSystems.DeactivateReactiveSystems();
            mainMenuSystems.TearDown();

            GameSystemService.RemoveActiveSystems(mainMenuSystems);
        }

        GameEntity hideUiEntity = context.CreateEntity();
        hideUiEntity.AddHideUi(new[] {AssetTypes.MainMenu});
    }
}