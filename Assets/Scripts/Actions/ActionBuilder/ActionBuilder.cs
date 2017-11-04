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
            }
        };

    private IActionPropertyAdder[] currentSequence;
    private int currentIndex;
    private GameContext context;
    private GameEntity actionEntity;
    private Action<GameEntity> successCallback;
    private Action<string> errorCallback;

    public void ChooseActionSequence(GameEntity actionEntity, GameContext context,
        Action<GameEntity> successCallback, Action<string> errorCallback)
    {
        this.actionEntity = actionEntity;
        this.context = context;
        this.successCallback = successCallback;
        this.errorCallback = errorCallback;

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
}