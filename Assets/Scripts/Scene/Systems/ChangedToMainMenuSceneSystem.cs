using System.Collections.Generic;
using Entitas;

public class ChangedToMainMenuSceneSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ChangedToMainMenuSceneSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SceneChanged);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.sceneChanged.NewSceneName == GameSceneConstants.MainMenuScene;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceGameState(GameState.MainMenu);
        UIService.HideWidget(AssetTypes.ReturnButton);
    }
}