//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TimeUntilActionComponent timeUntilAction { get { return (TimeUntilActionComponent)GetComponent(GameComponentsLookup.TimeUntilAction); } }
    public bool hasTimeUntilAction { get { return HasComponent(GameComponentsLookup.TimeUntilAction); } }

    public void AddTimeUntilAction(float newRemainingTime, float newTotalTime) {
        var index = GameComponentsLookup.TimeUntilAction;
        var component = CreateComponent<TimeUntilActionComponent>(index);
        component.RemainingTime = newRemainingTime;
        component.TotalTime = newTotalTime;
        AddComponent(index, component);
    }

    public void ReplaceTimeUntilAction(float newRemainingTime, float newTotalTime) {
        var index = GameComponentsLookup.TimeUntilAction;
        var component = CreateComponent<TimeUntilActionComponent>(index);
        component.RemainingTime = newRemainingTime;
        component.TotalTime = newTotalTime;
        ReplaceComponent(index, component);
    }

    public void RemoveTimeUntilAction() {
        RemoveComponent(GameComponentsLookup.TimeUntilAction);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTimeUntilAction;

    public static Entitas.IMatcher<GameEntity> TimeUntilAction {
        get {
            if(_matcherTimeUntilAction == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TimeUntilAction);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTimeUntilAction = matcher;
            }

            return _matcherTimeUntilAction;
        }
    }
}
