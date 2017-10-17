using System;

public class CharacterChooserItemProperties : IWidgetProperties
{
    public ActionType ActionType;
    public string ButtonText;
    public Action<ActionType> Callback;

    public CharacterChooserItemProperties(ActionType actionType, string buttonText, Action<ActionType> callback)
    {
        ActionType = actionType;
        ButtonText = buttonText;
        Callback = callback;
    }
}