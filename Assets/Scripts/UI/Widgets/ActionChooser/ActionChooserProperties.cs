using Entitas;

public class ActionChooserProperties : IWidgetProperties
{
    public int EntityId;
    public ActionType[] ActionTypes;
    public GameContext Context;

    public ActionChooserProperties(int entityId, ActionType[] actionTypes, GameContext context)
    {
        EntityId = entityId;
        ActionTypes = actionTypes;
        Context = context;
    }
}