//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpeedComponent speed { get { return (SpeedComponent)GetComponent(GameComponentsLookup.Speed); } }
    public bool hasSpeed { get { return HasComponent(GameComponentsLookup.Speed); } }

    public void AddSpeed(int newSpeedValue) {
        var index = GameComponentsLookup.Speed;
        var component = CreateComponent<SpeedComponent>(index);
        component.SpeedValue = newSpeedValue;
        AddComponent(index, component);
    }

    public void ReplaceSpeed(int newSpeedValue) {
        var index = GameComponentsLookup.Speed;
        var component = CreateComponent<SpeedComponent>(index);
        component.SpeedValue = newSpeedValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSpeed() {
        RemoveComponent(GameComponentsLookup.Speed);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.MatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherSpeed;

    public static Entitas.IMatcher<GameEntity> Speed {
        get {
            if(_matcherSpeed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Speed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpeed = matcher;
            }

            return _matcherSpeed;
        }
    }
}