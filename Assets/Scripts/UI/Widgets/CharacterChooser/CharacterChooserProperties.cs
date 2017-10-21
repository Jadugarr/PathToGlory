using Entitas;

public class CharacterChooserProperties : IWidgetProperties
{
    public int ChoosingEntityId;
    public int[] PossibleEntityIds;
    public GameContext Context;

    public CharacterChooserProperties(int choosingEntityId, int[] possibleEntityIds, GameContext context)
    {
        ChoosingEntityId = choosingEntityId;
        PossibleEntityIds = possibleEntityIds;
        Context = context;
    }
}