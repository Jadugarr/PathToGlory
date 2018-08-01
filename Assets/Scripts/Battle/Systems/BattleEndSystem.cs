using System.Collections.Generic;
using Entitas;

public class BattleEndSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public BattleEndSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.WinConditionsFulfilled,
            GameMatcher.LoseConditionsFulfilled));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isWinConditionsFulfilled || entity.isLoseConditionsFulfilled;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.BattleEnd);
        context.CreateEntity().AddBattleEnd(entities[0].isWinConditionsFulfilled);
    }
}