//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ActionFinishedComponent actionFinishedComponent = new ActionFinishedComponent();

    public bool isActionFinished {
        get { return HasComponent(GameComponentsLookup.ActionFinished); }
        set {
            if (value != isActionFinished) {
                var index = GameComponentsLookup.ActionFinished;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : actionFinishedComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
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

    static Entitas.IMatcher<GameEntity> _matcherActionFinished;

    public static Entitas.IMatcher<GameEntity> ActionFinished {
        get {
            if (_matcherActionFinished == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActionFinished);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActionFinished = matcher;
            }

            return _matcherActionFinished;
        }
    }
}
