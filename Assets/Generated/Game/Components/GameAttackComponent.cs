//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AttackComponent attack { get { return (AttackComponent)GetComponent(GameComponentsLookup.Attack); } }
    public bool hasAttack { get { return HasComponent(GameComponentsLookup.Attack); } }

    public void AddAttack(int newAttackValue) {
        var index = GameComponentsLookup.Attack;
        var component = CreateComponent<AttackComponent>(index);
        component.AttackValue = newAttackValue;
        AddComponent(index, component);
    }

    public void ReplaceAttack(int newAttackValue) {
        var index = GameComponentsLookup.Attack;
        var component = CreateComponent<AttackComponent>(index);
        component.AttackValue = newAttackValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAttack() {
        RemoveComponent(GameComponentsLookup.Attack);
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

    static Entitas.IMatcher<GameEntity> _matcherAttack;

    public static Entitas.IMatcher<GameEntity> Attack {
        get {
            if (_matcherAttack == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Attack);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttack = matcher;
            }

            return _matcherAttack;
        }
    }
}
