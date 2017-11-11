//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly DefendComponent defendComponent = new DefendComponent();

    public bool isDefend {
        get { return HasComponent(GameComponentsLookup.Defend); }
        set {
            if (value != isDefend) {
                if (value) {
                    AddComponent(GameComponentsLookup.Defend, defendComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Defend);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherDefend;

    public static Entitas.IMatcher<GameEntity> Defend {
        get {
            if (_matcherDefend == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Defend);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDefend = matcher;
            }

            return _matcherDefend;
        }
    }
}