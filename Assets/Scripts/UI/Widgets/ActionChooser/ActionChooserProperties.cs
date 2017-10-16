using Entitas;

public class ActionChooserProperties : IWidgetProperties
{
    public ActionType[] ActionTypes;
    public GameContext Context;

    public ActionChooserProperties(ActionType[] actionTypes, GameContext context)
    {
        ActionTypes = actionTypes;
        Context = context;
    }
}