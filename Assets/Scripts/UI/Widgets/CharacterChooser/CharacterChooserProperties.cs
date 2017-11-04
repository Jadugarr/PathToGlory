using Entitas;

public class CharacterChooserProperties : IWidgetProperties
{
    public int[] PossibleEntityIds;
    public GameContext Context;

    public CharacterChooserProperties(int[] possibleEntityIds, GameContext context)
    {
        PossibleEntityIds = possibleEntityIds;
        Context = context;
    }
}