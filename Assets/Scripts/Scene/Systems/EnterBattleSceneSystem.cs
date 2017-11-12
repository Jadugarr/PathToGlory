using System.Collections.Generic;
using Configurations;
using Entitas;

public class EnterBattleSceneSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterBattleSceneSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SceneChanged);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.sceneChanged.NewSceneName == GameSceneConstants.BattleScene;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameConfigurations.InputConfiguration.ChangeActiveInputMap(GameState.Battle);
        context.ReplaceGameState(GameState.Battle);
    }
}