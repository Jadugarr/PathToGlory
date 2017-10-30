using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ATBBarWidget : AWidget
{
    [SerializeField] private GameObject StartMarker;
    [SerializeField] private GameObject ChooseMarker;
    [SerializeField] private GameObject ActMarker;

    // Distance between ATB start point and point where characters can choose an action
    private float chooseCommandDistance = 0f;
    // Distance between chosen action marker and actual execution marker
    private float executeActionDistance = 0f;

    private IGroup<GameEntity> battleEntityGroup;
    private IGroup<GameEntity> timeLeftEntityGroup;
    private List<ATBItemWidget> atbItems = new List<ATBItemWidget>(6);
    private GameObject itemPrefab;

    public override void Open()
    {
        itemPrefab = UIService.GetAsset(AssetTypes.AtbItem);
        chooseCommandDistance = ChooseMarker.transform.localPosition.x - StartMarker.transform.localPosition.x;
        executeActionDistance = ActMarker.transform.localPosition.x - ChooseMarker.transform.localPosition.x;
        
        battleEntityGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.Battle);
        //timeLeftEntityGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.TimeUntilAction);

        timeLeftEntityGroup.OnEntityUpdated += OnTimeLeftEntityUpdated;
        battleEntityGroup.OnEntityAdded += OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved += OnBattleEntityRemoved;
    }

    public override void Close()
    {
        DestroyItems();
        timeLeftEntityGroup.OnEntityUpdated -= OnTimeLeftEntityUpdated;
        battleEntityGroup.OnEntityAdded -= OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved -= OnBattleEntityRemoved;

        battleEntityGroup = null;
        timeLeftEntityGroup = null;
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
        DestroyItems();
        InitItems();
    }

    private void DestroyItems()
    {
        for (int i = atbItems.Count - 1; i >= 0; i--)
        {
            atbItems[i].Close();
            Destroy(atbItems[i].gameObject);
        }

        atbItems.Clear();
    }

    private void OnTimeLeftEntityUpdated(IGroup<GameEntity> @group, GameEntity entity, int index,
        IComponent previousComponent, IComponent newComponent)
    {
        //ATBItemWidget linkedItem = GetLinkedItem(entity.id.Id);
        //TimeUntilActionComponent timeComponent = entity.timeUntilAction;
        //float progressPercentage = 1 - (timeComponent.RemainingTime / timeComponent.TotalTime);
        //linkedItem.gameObject.transform.localPosition = new Vector3(StartMarker.transform.localPosition.x +
        //                                                 (chooseCommandDistance * progressPercentage),
        //    linkedItem.transform.localPosition.y, linkedItem.transform.localPosition.z);
    }

    private void InitItems()
    {
        GameEntity[] entities = battleEntityGroup.GetEntities();

        foreach (GameEntity gameEntity in entities)
        {
            CreateNewItem(gameEntity);
        }
    }

    private void CreateNewItem(GameEntity gameEntity)
    {
        ATBItemProperties newProps;

        if (gameEntity.hasBattleImage)
        {
            newProps = new ATBItemProperties(gameEntity.battleImage.BattleImage, gameEntity.id.Id);
        }
        else
        {
            newProps = new ATBItemProperties(new Sprite(), gameEntity.id.Id);
        }

        ATBItemWidget newItem = GameObject.Instantiate(itemPrefab, gameObject.transform)
            .GetComponent<ATBItemWidget>();
        Vector3 itemPosition = new Vector3(StartMarker.transform.position.x, newItem.gameObject.transform.position.y,
            newItem.gameObject.transform.position.z);
        newItem.gameObject.transform.SetPositionAndRotation(itemPosition,
            StartMarker.transform.rotation);
        newItem.Open();
        newItem.ApplyProperties(newProps);
        atbItems.Add(newItem);
    }

    private ATBItemWidget GetLinkedItem(int entityId)
    {
        foreach (ATBItemWidget atbItemWidget in atbItems)
        {
            if (atbItemWidget.GetLinkedCharacterId() == entityId)
            {
                return atbItemWidget;
            }
        }

        return null;
    }

    private void OnBattleEntityRemoved(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        ATBItemWidget item = GetLinkedItem(entity.id.Id);
        item.Close();
        atbItems.Remove(item);
        Destroy(item.gameObject);
    }

    private void OnBattleEntityAdded(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        CreateNewItem(entity);
    }

    protected override void OnShow()
    {
        timeLeftEntityGroup.OnEntityUpdated += OnTimeLeftEntityUpdated;
        battleEntityGroup.OnEntityAdded += OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved += OnBattleEntityRemoved;
    }

    protected override void OnHide()
    {
        timeLeftEntityGroup.OnEntityUpdated -= OnTimeLeftEntityUpdated;
        battleEntityGroup.OnEntityAdded -= OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved -= OnBattleEntityRemoved;
    }
}