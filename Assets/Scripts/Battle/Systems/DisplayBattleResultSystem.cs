using System.Collections.Generic;
using Entitas;

public class DisplayBattleResultSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public DisplayBattleResultSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
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
        string textToDisplay = entities[0].battleEnd.HasPlayerWon ? "I fucking won!" : "JESUS I LOST!";

        var displayResultEntity = context.CreateEntity();
        displayResultEntity.AddDisplayUI(AssetTypes.BattleResultText, new BattleResultWidgetProperties(textToDisplay));
    }
}