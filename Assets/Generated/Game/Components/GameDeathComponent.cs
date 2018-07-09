//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DeathComponent death { get { return (DeathComponent)GetComponent(GameComponentsLookup.Death); } }
    public bool hasDeath { get { return HasComponent(GameComponentsLookup.Death); } }

    public void AddDeath(GameEntity newDeadCharacter) {
        var index = GameComponentsLookup.Death;
        var component = CreateComponent<DeathComponent>(index);
        component.DeadCharacter = newDeadCharacter;
        AddComponent(index, component);
    }

    public void ReplaceDeath(GameEntity newDeadCharacter) {
        var index = GameComponentsLookup.Death;
        var component = CreateComponent<DeathComponent>(index);
        component.DeadCharacter = newDeadCharacter;
        ReplaceComponent(index, component);
    }

    public void RemoveDeath() {
        RemoveComponent(GameComponentsLookup.Death);
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

    static Entitas.IMatcher<GameEntity> _matcherDeath;

    public static Entitas.IMatcher<GameEntity> Death {
        get {
            if (_matcherDeath == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Death);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDeath = matcher;
            }

            return _matcherDeath;
        }
    }
}
