using System.Collections.Generic;
using Entitas;

public class HideUiSystem : ReactiveSystem<GameEntity>
{
    public HideUiSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.HideUi);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            foreach (string assetName in gameEntity.hideUi.AssetName)
            {
                UIService.HideWidget(assetName);
            }
        }
    }
}