using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ATBBarWidget : AWidget
{
    [SerializeField] private GameObject StartMarker;
    [SerializeField] private GameObject ChooseMarker;
    [SerializeField] private GameObject ActMarker;

    private IGroup<GameEntity> battleEntityGroup;
    private List<ATBItemBehaviour> atbItems = new List<ATBItemBehaviour>(6);

    public override void Open()
    {
    }

    public override void Close()
    {
    }

    public override string GetName()
    {
        return AssetTypes.Atb;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Dynamic;
    }

    protected override void OnNewProperties()
    {
        ATBBarProperties props = (ATBBarProperties) properties;

        if (battleEntityGroup != null)
        {
            battleEntityGroup.OnEntityAdded -= BattleEntityGroupOnOnEntityAdded;
            battleEntityGroup.OnEntityRemoved -= BattleEntityGroupOnOnEntityRemoved;
        }

        battleEntityGroup = props.context.GetGroup(GameMatcher.Battle);

        battleEntityGroup.OnEntityAdded += BattleEntityGroupOnOnEntityAdded;
        battleEntityGroup.OnEntityRemoved += BattleEntityGroupOnOnEntityRemoved;
    }

    private void BattleEntityGroupOnOnEntityRemoved(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        throw new NotImplementedException();
    }

    private void BattleEntityGroupOnOnEntityAdded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        throw new NotImplementedException();
    }
}