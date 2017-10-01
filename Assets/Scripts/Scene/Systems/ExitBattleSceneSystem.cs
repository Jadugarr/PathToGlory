using System.Collections.Generic;
using Entitas;

public class ExitBattleSceneSystem : ReactiveSystem<GameEntity>
{
    public ExitBattleSceneSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SceneChanged);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.sceneChanged.PreviousSceneName == GameSceneConstants.BattleScene;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        UIService.HideWidget(AssetTypes.ReturnButton);
        UIService.HideWidget(AssetTypes.Atb);
    }
}