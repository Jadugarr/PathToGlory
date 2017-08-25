using System.Collections.Generic;
using Entitas;

public class ExitMainMenuSceneSystem : ReactiveSystem<GameEntity>
{
    public ExitMainMenuSceneSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SceneChanged);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.sceneChanged.PreviousSceneName == GameSceneConstants.MainMenuScene;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        UIService.HideWidget(AssetTypes.MainMenu);
    }
}