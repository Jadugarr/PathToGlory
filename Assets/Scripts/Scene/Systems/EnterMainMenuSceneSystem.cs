using System.Collections.Generic;
using Entitas;

public class EnterMainMenuSceneSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterMainMenuSceneSystem(GameContext context) : base(context)
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
        GameEntity newEntity = context.CreateEntity();
        newEntity.ReplaceGameState(GameState.MainMenu);
    }
}