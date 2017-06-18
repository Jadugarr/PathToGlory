//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DefenseComponent defense { get { return (DefenseComponent)GetComponent(GameComponentsLookup.Defense); } }
    public bool hasDefense { get { return HasComponent(GameComponentsLookup.Defense); } }

    public void AddDefense(int newDefenseValue) {
        var index = GameComponentsLookup.Defense;
        var component = CreateComponent<DefenseComponent>(index);
        component.DefenseValue = newDefenseValue;
        AddComponent(index, component);
    }

    public void ReplaceDefense(int newDefenseValue) {
        var index = GameComponentsLookup.Defense;
        var component = CreateComponent<DefenseComponent>(index);
        component.DefenseValue = newDefenseValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDefense() {
        RemoveComponent(GameComponentsLookup.Defense);
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

    static Entitas.IMatcher<GameEntity> _matcherDefense;

    public static Entitas.IMatcher<GameEntity> Defense {
        get {
            if(_matcherDefense == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Defense);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDefense = matcher;
            }

            return _matcherDefense;
        }
    }
}