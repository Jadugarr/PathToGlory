using System.Collections.Generic;
using Entitas;

public class ExitPausedSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitPausedSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return context.subState.PreviousSubState == SubState.Paused;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity hidePauseOverlayEntity = context.CreateEntity();
        hidePauseOverlayEntity.AddHideUi(new[] {AssetTypes.PauseOverlay});
    }
}