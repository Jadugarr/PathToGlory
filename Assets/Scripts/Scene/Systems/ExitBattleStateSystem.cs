using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.Unity;

public class ExitBattleStateSystem : GameReactiveSystem
{
    private GameContext context;
    private IGroup<GameEntity> viewGroup;

    public ExitBattleStateSystem(GameContext context) : base(context)
    {
        this.context = context;
        viewGroup = this.context.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity entity in viewGroup.GetEntities())
        {
            entity.view.View.Unlink();
        }
        
        Systems battleSystems = GameSystemService.GetSystemMapping(GameState.Battle);
        if (battleSystems != null)
        {
            GameSystemService.RemoveActiveSystems(battleSystems);
        }

        UIService.HideWidget(new[]
            {AssetTypes.ReturnButton, AssetTypes.Atb, AssetTypes.ActionChooser, AssetTypes.BattleResultText});
        context.SetNewSubstate(SubState.Undefined);
        GameEntity unloadSceneEntity = context.CreateEntity();
        unloadSceneEntity.AddUnloadScene(GameSceneConstants.BattleScene);
    }
}