﻿using System.Collections.Generic;
using UnityEngine;

public class ActionChooserWidget : AWidget
{
    private List<ActionChooserItemWidget> chooseItems = new List<ActionChooserItemWidget>();
    private GameObject chooseItemPrefab;

    public override void Open()
    {
    }

    public override void Close()
    {
        foreach (ActionChooserItemWidget actionChooserItemWidget in chooseItems)
        {
            actionChooserItemWidget.Close();
        }
    }

    protected override void OnShow()
    {
        foreach (ActionChooserItemWidget actionChooserItemWidget in chooseItems)
        {
            actionChooserItemWidget.Show();
        }
    }

    protected override void OnHide()
    {
        foreach (ActionChooserItemWidget actionChooserItemWidget in chooseItems)
        {
            actionChooserItemWidget.Hide();
        }
    }

    protected override void OnNewProperties()
    {
        DestroyItems();

        ActionChooserProperties props = (ActionChooserProperties) properties;

        if (chooseItemPrefab == null)
        {
            chooseItemPrefab = UIService.GetAsset(AssetTypes.ActionChooserItem);
        }

        foreach (ActionType propsActionType in props.ActionTypes)
        {
            ActionChooserItemWidget newItem = Instantiate(chooseItemPrefab, gameObject.transform)
                .GetComponent<ActionChooserItemWidget>();
            newItem.Open();
            newItem.ApplyProperties(
                new ActionChooserItemProperties(propsActionType, propsActionType.ToString(), OnItemClicked));
            chooseItems.Add(newItem);
        }
    }

    public override string GetName()
    {
        return AssetTypes.ActionChooser;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    private void DestroyItems()
    {
        if (chooseItems.Count > 0)
        {
            for (int i = chooseItems.Count - 1; i >= 0; i--)
            {
                chooseItems[i].Close();
                Destroy(chooseItems[i].gameObject);
            }
            chooseItems.Clear();
        }
    }

    private void OnItemClicked(ActionType actionType)
    {
        ActionChooserProperties props = (ActionChooserProperties) properties;
        GameEntity actionChosenEntity = props.Context.CreateEntity();
        actionChosenEntity.AddChoseAction(props.EntityId, actionType);
        UIService.HideWidget(AssetTypes.ActionChooser);
    }
}