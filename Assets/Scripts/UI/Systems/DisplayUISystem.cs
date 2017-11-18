using System.Collections.Generic;
using Entitas;

public class DisplayUISystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private IGroup<GameEntity> uiComponentGroup;

    public DisplayUISystem(IContext<GameEntity> context) : base(context)
    {
        uiComponentGroup = context.GetGroup(GameMatcher.DisplayUI);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DisplayUI);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            UIService.ShowWidget(entity.displayUI.AssetName, entity.displayUI.properties);
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in uiComponentGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}