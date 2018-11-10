using System.Collections.Generic;
using Entitas;

public class ExitPausedSubStateSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.HideWidget(AssetTypes.PauseOverlay);
    }
}