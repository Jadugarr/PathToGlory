//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ReadyToActComponent readyToAct { get { return (ReadyToActComponent)GetComponent(GameComponentsLookup.ReadyToAct); } }
    public bool hasReadyToAct { get { return HasComponent(GameComponentsLookup.ReadyToAct); } }

    public void AddReadyToAct(GameEntity newEntityReadyToAct) {
        var index = GameComponentsLookup.ReadyToAct;
        var component = CreateComponent<ReadyToActComponent>(index);
        component.EntityReadyToAct = newEntityReadyToAct;
        AddComponent(index, component);
    }

    public void ReplaceReadyToAct(GameEntity newEntityReadyToAct) {
        var index = GameComponentsLookup.ReadyToAct;
        var component = CreateComponent<ReadyToActComponent>(index);
        component.EntityReadyToAct = newEntityReadyToAct;
        ReplaceComponent(index, component);
    }

    public void RemoveReadyToAct() {
        RemoveComponent(GameComponentsLookup.ReadyToAct);
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

    static Entitas.IMatcher<GameEntity> _matcherReadyToAct;

    public static Entitas.IMatcher<GameEntity> ReadyToAct {
        get {
            if (_matcherReadyToAct == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReadyToAct);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReadyToAct = matcher;
            }

            return _matcherReadyToAct;
        }
    }
}
