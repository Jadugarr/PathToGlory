using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ATBBarWidget : AWidget
{
    [SerializeField] private GameObject StartMarker;
    [SerializeField] private GameObject ChooseMarker;
    [SerializeField] private GameObject ActMarker;

    private IGroup<GameEntity> battleEntityGroup;
    private IGroup<GameEntity> timeLeftEntityGroup;
    private List<ATBItemWidget> atbItems = new List<ATBItemWidget>(6);
    private GameObject itemPrefab;

    public override void Open()
    {
        itemPrefab = UIService.GetAsset(AssetTypes.AtbItem);
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
            for (int i = atbItems.Count - 1; i >= 0; i--)
            {
                atbItems[i].Close();
                Destroy(atbItems[i].gameObject);
            }
        }

        battleEntityGroup = props.context.GetGroup(GameMatcher.Battle);

        battleEntityGroup.OnEntityAdded += BattleEntityGroupOnOnEntityAdded;
        battleEntityGroup.OnEntityRemoved += BattleEntityGroupOnOnEntityRemoved;
        InitItems();
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
        ATBItemProperties newProps = new ATBItemProperties(new Sprite(), gameEntity.id.Id);
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

    private void BattleEntityGroupOnOnEntityRemoved(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        ATBItemWidget item = GetLinkedItem(entity.id.Id);
        item.Close();
        atbItems.Remove(item);
        Destroy(item.gameObject);
    }

    private void BattleEntityGroupOnOnEntityAdded(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        CreateNewItem(entity);
    }
}