//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ExecuteActionComponent executeActionComponent = new ExecuteActionComponent();

    public bool isExecuteAction {
        get { return HasComponent(GameComponentsLookup.ExecuteAction); }
        set {
            if (value != isExecuteAction) {
                var index = GameComponentsLookup.ExecuteAction;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : executeActionComponent;

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
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherExecuteAction;

    public static Entitas.IMatcher<GameEntity> ExecuteAction {
        get {
            if (_matcherExecuteAction == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ExecuteAction);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherExecuteAction = matcher;
            }

            return _matcherExecuteAction;
        }
    }
}
