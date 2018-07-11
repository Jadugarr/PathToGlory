using System;
using System.Collections.Generic;

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

    private IActionPropertyAdder[] currentSequence;
    private int currentIndex;
    private GameContext context;
    private GameEntity actionEntity;
    private Action<GameEntity> successCallback;
    private Action<string> errorCallback;
    private Action cancelCallback;

    public void ChooseActionSequence(GameEntity actionEntity, GameContext context,
        Action<GameEntity> successCallback, Action<string> errorCallback, Action cancelCallback)
    {
        this.actionEntity = actionEntity;
        this.context = context;
        this.successCallback = successCallback;
        this.errorCallback = errorCallback;
        this.cancelCallback = cancelCallback;

        DisplayChoices();

        if (actionSequenceMap.ContainsKey(actionEntity.battleAction.ActionType))
        {
            currentSequence = actionSequenceMap[actionEntity.battleAction.ActionType];
            currentIndex = 0;
            ExecuteNextStep();
        }
        else
        {
            errorCallback("Sequence map didn't contain action type: " + actionEntity.battleAction.ActionType);
        }
    }

    private void ExecuteNextStep()
    {
        if (currentIndex < currentSequence.Length)
        {
            currentSequence[currentIndex].Execute(context, actionEntity, OnSuccess, OnError);
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
        if (currentSequence != null && currentIndex < currentSequence.Length)
        {
            currentSequence[currentIndex].Cancel();
        }

        currentSequence = null;
        currentIndex = 0;
        context = null;
        actionEntity = null;
        successCallback = null;
        errorCallback = null;
        cancelCallback = null;
    }

    private void OnSuccess()
    {
        currentIndex++;
        ExecuteNextStep();
    }

    private void OnError(string error)
    {
        errorCallback(error);
        Reset();
    }

    private void DisplayChoices()
    {
        GameEntity choosingEntity = context.GetEntityWithId(actionEntity.battleAction.EntityId);
        GameEntity displayUi = context.CreateEntity();
        displayUi.AddDisplayUI(AssetTypes.ActionChooser,
            new ActionChooserProperties(choosingEntity.id.Id,
                actionEntity.battleActionChoices.BattleActionChoices.ToArray(),
                context));
    }

    public void Cancel()
    {
        if (currentSequence != null)
        {
            if (currentIndex > 0)
            {
                currentSequence[currentIndex].Cancel();
                currentIndex--;
                currentSequence[currentIndex].Execute(context, actionEntity, OnSuccess, OnError);
            }
            else
            {
                cancelCallback();
                Reset();
            }
        }
    }
}