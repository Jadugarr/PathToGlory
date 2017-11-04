using System.Collections.Generic;
using Entitas;

public class ExitBattleSceneSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitBattleSceneSystem(GameContext context) : base(context)
    {
        this.context = context;
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
        GameEntity hideUiEntity = context.CreateEntity();
        hideUiEntity.AddHideUi(new[] {AssetTypes.ReturnButton, AssetTypes.Atb, AssetTypes.ActionChooser});
    }
}