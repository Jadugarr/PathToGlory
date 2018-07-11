using Entitas;

public class ActionChooserProperties : IWidgetProperties
{
    public int EntityId;
    public BattleActionChoice[] ActionChoices;
    public GameContext Context;

    public ActionChooserProperties(int entityId, BattleActionChoice[] actionChoices, GameContext context)
    {
        EntityId = entityId;
        ActionChoices = actionChoices;
        Context = context;
    }
}