namespace Entitas.Battle.Systems
{
    public class InitializeChooseActionSystem : IInitializeSystem
    {
        private GameContext context;

        public InitializeChooseActionSystem(GameContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            IGroup<GameEntity> choosingEntities =
                context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
            GameEntity currentEntity = null;
            foreach (GameEntity choosingEntity in choosingEntities)
            {
                if (choosingEntity.executionTime.RemainingTime <= 0f)
                {
                    currentEntity = choosingEntity;
                    break;
                }
            }

            if (currentEntity != null)
            {
                GameEntity characterEntity = context.GetEntityWithId(currentEntity.battleAction.EntityId);
                UIService.ShowWidget(AssetTypes.ActionChooser,
                    new ActionChooserProperties(currentEntity,
                        characterEntity.battleActionChoices.BattleActionChoices.ToArray(), context));
            }
            else
            {
                context.ReplaceSubState(context.subState.CurrentSubState, SubState.Waiting);
            }
        }
    }
}