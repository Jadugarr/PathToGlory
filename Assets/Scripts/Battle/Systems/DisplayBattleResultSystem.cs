using System.Collections.Generic;
using Entitas;

public class DisplayBattleResultSystem : ReactiveSystem<GameEntity>
{

    public DisplayBattleResultSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.BattleEnd);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        string textToDisplay = entities[0].battleEnd.HasPlayerWon ? "I won!" : "JESUS I LOST!";

        UIService.ShowWidget(AssetTypes.BattleResultText, new BattleResultWidgetProperties(textToDisplay));
    }
}