using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ActionChosenSystem : GameReactiveSystem
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        context.SetNewSubstate(SubState.ChooseTarget);
    }
}