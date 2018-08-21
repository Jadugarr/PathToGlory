using System;
using System.Collections.Generic;
using Entitas;

public class ActionBuilder
{
    private static ActionBuilder instance = new ActionBuilder();

    public static ActionBuilder Instance
    {
        get { return instance; }
    }

    private ActionBuilder()
    {
    }

    private readonly Dictionary<ActionType, IActionPropertyAdder[]> actionSequenceMap =
        new Dictionary<ActionType, IActionPropertyAdder[]>()
        {
            {
                ActionType.AttackCharacter,
                new IActionPropertyAdder[] {new ChooseCharacterPropertyAdder(), new AttackCharacterTimePropertyAdder()}
            },
            {
                ActionType.Defend,
                new[] {new DefenseActionTimePropertyAdder()}
            }
        };

    private Systems actionBuilderSystems;
    private IActionPropertyAdder[] currentSequence;
    private IGroup<GameEntity> choseActionGroup;
    private int currentSequenceStep;
    private GameContext context;
    private GameEntity actionEntity;
    private GameEntity choosingEntity;
    private Action<GameEntity> successCallback;
    private Action<string> errorCallback;

    private bool isPlayerAction;

    public void ChooseActionSequence(GameEntity actionEntity, GameContext context,
        Action<GameEntity> successCallback, Action<string> errorCallback, bool isPlayerAction = true)
    {
        this.actionEntity = actionEntity;
        this.context = context;
        this.successCallback = successCallback;
        this.errorCallback = errorCallback;
        this.isPlayerAction = isPlayerAction;

        if (actionBuilderSystems == null)
        {
            actionBuilderSystems = new Feature("ActionBuilderSystems")
                .Add(new ProcessBattleCancelInputSystem(context));
        }

        choseActionGroup = context.GetGroup(GameMatcher.ChoseAction);
        StartActionBuilding();
    }

    private void StartActionBuilding()
    {
        if (isPlayerAction)
        {
            DisplayChoices();
        }
        else
        {
            choosingEntity = context.GetEntityWithId(actionEntity.battleAction.EntityId);
            GameEntity choseActionEntity = context.CreateEntity();
            choseActionEntity.AddChoseAction(choosingEntity.id.Id, ActionType.Defend);
            ExecuteChosenAction(choseActionEntity.choseAction);
        }
    }

    private void ExecuteNextStep()
    {
        if (currentSequenceStep < currentSequence.Length)
        {
            if (isPlayerAction)
            {
                currentSequence[currentSequenceStep].Execute(context, actionEntity, OnSuccess, OnError);
            }
            else
            {
                currentSequence[currentSequenceStep].ExecuteEnemyLogic(context, actionEntity, OnSuccess, OnError);
            }
        }
        else
        {
            SequenceFinished();
        }
    }

    private void SequenceFinished()
    {
        successCallback(actionEntity);

        Reset();
    }

    public void Reset()
    {
        if (currentSequence != null && currentSequenceStep < currentSequence.Length)
        {
            currentSequence[currentSequenceStep].Cancel();
        }

        currentSequence = null;
        currentSequenceStep = 0;
        context = null;
        actionEntity = null;
        choosingEntity = null;
        successCallback = null;
        errorCallback = null;
        if (choseActionGroup != null)
        {
            choseActionGroup.OnEntityAdded -= OnChoseAction;
            choseActionGroup = null;
        }

        actionBuilderSystems.DeactivateReactiveSystems();
        GameSystemService.RemoveActiveSystems(actionBuilderSystems);
    }

    private void OnSuccess()
    {
        currentSequenceStep++;
        ExecuteNextStep();
    }

    private void OnError(string error)
    {
        errorCallback(error);
        Reset();
    }

    private void DisplayChoices()
    {
        choosingEntity = context.GetEntityWithId(actionEntity.battleAction.EntityId);
        GameEntity displayUi = context.CreateEntity();
        displayUi.AddDisplayUI(AssetTypes.ActionChooser,
            new ActionChooserProperties(choosingEntity.id.Id,
                choosingEntity.battleActionChoices.BattleActionChoices.ToArray(),
                context));

        choseActionGroup.OnEntityAdded += OnChoseAction;
        actionBuilderSystems.DeactivateReactiveSystems();
        GameSystemService.RemoveActiveSystems(actionBuilderSystems);
    }

    private void OnChoseAction(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        if (entity.hasChoseAction)
        {
            ExecuteChosenAction(entity.choseAction);
        }
    }

    private void ExecuteChosenAction(ChoseActionComponent choseActionComponent)
    {
        if (choseActionComponent.EntityId == choosingEntity.id.Id)
        {
            actionEntity.battleAction.ActionType = choseActionComponent.ActionType;
            if (actionSequenceMap.TryGetValue(actionEntity.battleAction.ActionType, out currentSequence))
            {
                actionBuilderSystems.ActivateReactiveSystems();
                GameSystemService.AddActiveSystems(actionBuilderSystems);
                choseActionGroup.OnEntityAdded -= OnChoseAction;
                currentSequenceStep = 0;
                ExecuteNextStep();
            }
            else
            {
                errorCallback("Sequence map didn't contain action type: " + actionEntity.battleAction.ActionType);
            }
        }
    }

    public void Cancel()
    {
        if (currentSequence != null)
        {
            if (currentSequenceStep < currentSequence.Length)
            {
                currentSequence[currentSequenceStep].Cancel();
            }

            if (currentSequenceStep > 0)
            {
                currentSequenceStep--;
                currentSequence[currentSequenceStep].Execute(context, actionEntity, OnSuccess, OnError);
            }
            else
            {
                StartActionBuilding();
            }
        }
    }
}