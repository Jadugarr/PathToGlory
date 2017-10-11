public class ChooseActionProperties : IBattleActionProperties
{
    public GameEntity ChoosingEntity;

    public ChooseActionProperties(GameEntity choosingEntity)
    {
        ChoosingEntity = choosingEntity;
    }
}