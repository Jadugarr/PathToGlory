using System.Collections.Generic;
using Entitas;

public class EnterChoosingSubStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public EnterChoosingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext)context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.Choosing;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        InputConfiguration.ChangeActiveSubStateInputMap(SubState.Choosing);
    }
}