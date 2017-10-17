using Entitas;

public class CharacterChooserProperties : IWidgetProperties
{
    public ActionType[] ActionTypes;
    public GameContext Context;

    public CharacterChooserProperties(ActionType[] actionTypes, GameContext context)
    {
        ActionTypes = actionTypes;
        Context = context;
    }
}