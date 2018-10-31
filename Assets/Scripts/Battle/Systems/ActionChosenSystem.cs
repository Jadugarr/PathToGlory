using System.Collections.Generic;
using Entitas;

public class ActionChosenSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ActionChosenSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.BattleAction, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionType != ActionType.None;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.ChooseTarget);
    }
}