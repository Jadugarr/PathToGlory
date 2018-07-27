using System.Collections.Generic;
using Entitas;

public class ExitBattleStateSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;

    public ExitBattleStateSystem(GameContext context) : base(context)
    {
        this.context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState == GameState.Battle;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Systems battleSystems = GameSystemService.GetSystemMapping(GameState.Battle);
        if (battleSystems != null)
        {
            battleSystems.ClearReactiveSystems();
            battleSystems.DeactivateReactiveSystems();
            battleSystems.TearDown();

            GameSystemService.RemoveActiveSystems(battleSystems);
        }

        GameEntity hideUiEntity = context.CreateEntity();
        hideUiEntity.AddHideUi(new[] {AssetTypes.ReturnButton, AssetTypes.Atb, AssetTypes.ActionChooser});
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.Undefined);
        GameEntity unloadSceneEntity = context.CreateEntity();
        unloadSceneEntity.AddUnloadScene(GameSceneConstants.BattleScene);
    }
}