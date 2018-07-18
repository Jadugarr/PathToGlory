using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecutePlayerChooseActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private bool isExecuting;
    private GameEntity currentActionEntity;
    private IGroup<GameEntity> gameStateGroup;

    private Queue<GameEntity> executeActionQueue = new Queue<GameEntity>();

    public ExecutePlayerChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
        gameStateGroup = context.GetGroup(GameMatcher.GameState);
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
        context.ReplaceSubState(context.subState.CurrentSubState, SubState.Choosing);

        foreach (GameEntity gameEntity in entities)
        {
            executeActionQueue.Enqueue(gameEntity);
        }

        if (isExecuting == false)
        {
            isExecuting = true;
            gameStateGroup.OnEntityUpdated += OnGameStateUpdated;
            ProcessQueue();
        }
    }

    private void ProcessQueue()
    {
        currentActionEntity = executeActionQueue.Dequeue();
        BattleActionComponent currentBattleAction = currentActionEntity.battleAction;
        GameEntity newActionEntity = context.CreateEntity();
        newActionEntity.AddBattleAction(currentBattleAction.EntityId, ActionType.None,
            ActionATBType.Acting);
        ActionBuilder.Instance.ChooseActionSequence(newActionEntity, context, OnSuccess, OnError);
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
        if (context.gameState.CurrentGameState != GameState.Battle &&
            context.subState.CurrentSubState != SubState.Paused)
        {
            Reset();
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
            gameStateGroup.OnEntityUpdated -= OnGameStateUpdated;
            context.ReplaceSubState(context.subState.CurrentSubState, SubState.Waiting);
        }
    }

    private void OnError(string error)
    {
        // TODO: Create an actual logging system, that doesn't just throw errors in the Unity console you lazy piece of shit
        Debug.LogError(error);
    }
}