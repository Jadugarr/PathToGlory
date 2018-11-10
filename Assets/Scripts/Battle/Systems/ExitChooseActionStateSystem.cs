using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitChooseActionStateSystem : GameReactiveSystem
{
    public ExitChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ChooseAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.ChooseAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseAction));
        }

        UIService.HideWidget(AssetTypes.ActionChooser);
    }
}