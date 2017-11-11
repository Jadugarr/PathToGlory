using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecutePlayerChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private bool isExecuting;
    private GameEntity currentActionEntity;
    private IGroup<GameEntity> choseActionGroup;
    private IGroup<GameEntity> gameStateGroup;

    private Queue<GameEntity> executeActionQueue = new Queue<GameEntity>();

    public ExecutePlayerChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
        choseActionGroup = context.GetGroup(GameMatcher.ChoseAction);
        gameStateGroup = context.GetGroup(GameMatcher.GameState);
        choseActionGroup.OnEntityAdded += OnChoseAction;
        gameStateGroup.OnEntityUpdated += OnGameStateUpdated;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecuteAction, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity executionerEntity = context.GetEntityWithId(entity.battleAction.EntityId);
        return entity.battleAction.ActionType == ActionType.ChooseAction && executionerEntity.isPlayer;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            executeActionQueue.Enqueue(gameEntity);
        }

        if (isExecuting == false)
        {
            isExecuting = true;
            ProcessQueue();
        }
    }

    private void ProcessQueue()
    {
        currentActionEntity = executeActionQueue.Dequeue();
        DisplayChoices();
    }

    private void DisplayChoices()
    {
        GameEntity choosingEntity = context.GetEntityWithId(currentActionEntity.battleAction.EntityId);
        GameEntity displayUi = context.CreateEntity();
        displayUi.AddDisplayUI(AssetTypes.ActionChooser,
            new ActionChooserProperties(choosingEntity.id.Id, new[] { ActionType.AttackCharacter, ActionType.Defend },
                context));
    }

    private void Reset()
    {
        executeActionQueue.Clear();
        currentActionEntity = null;
        isExecuting = false;
        ActionBuilder.Instance.Reset();
    }

    private void OnGameStateUpdated(IGroup<GameEntity> @group, GameEntity entity, int index,
        IComponent previousComponent, IComponent newComponent)
    {
        if (entity.gameState.CurrentGameState != GameState.Battle &&
            entity.gameState.CurrentGameState != GameState.Paused)
        {
            Reset();
        }
    }

    private void OnChoseAction(IGroup<GameEntity> @group, GameEntity choseActionEntity, int index, IComponent component)
    {
        if (isExecuting)
        {
            BattleActionComponent currentBattleAction = currentActionEntity.battleAction;
            GameEntity newActionEntity = context.CreateEntity();
            newActionEntity.AddBattleAction(currentBattleAction.EntityId, choseActionEntity.choseAction.ActionType,
                ActionATBType.Acting);
            ActionBuilder.Instance.ChooseActionSequence(newActionEntity, context, OnSuccess, OnError, OnCancel);
        }
        else
        {
            Debug.LogError("We're trying to choose an action although the queue isn't supposed to be active!");
        }
    }

    private void OnSuccess(GameEntity actionEntity)
    {
        currentActionEntity.Destroy();

        if (executeActionQueue.Count > 0)
        {
            ProcessQueue();
        }
        else
        {
            isExecuting = false;
        }
    }

    private void OnCancel()
    {
        DisplayChoices();
    }

    private void OnError(string error)
    {
        // TODO: Create an actual logging system, that doesn't just throw errors in the Unity console you lazy piece of shit
        Debug.LogError(error);
    }
}