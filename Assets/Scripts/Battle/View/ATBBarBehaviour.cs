using Entitas;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ATBBarBehaviour : MonoBehaviour
{
    private Slider slider;

    private GameContext context;
    private IGroup<GameEntity> timeLeftGroup;
    private IGroup<GameEntity> readyToActGroup;

    // Use this for initialization
    private void Awake()
    {
        context = Contexts.sharedInstance.game;
        timeLeftGroup = context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Player, GameMatcher.TimeUntilAction));
        readyToActGroup = context.GetGroup(GameMatcher.ReadyToAct);
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToActGroup.count <= 0)
        {
            TimeUntilActionComponent timeLeft = timeLeftGroup.GetEntities()[0].timeUntilAction;

            float newValue = 1f - (timeLeft.RemainingTime / timeLeft.TotalTime);
            slider.value = newValue;
        }
    }
}