//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AttackCharacterComponent attackCharacter { get { return (AttackCharacterComponent)GetComponent(GameComponentsLookup.AttackCharacter); } }
    public bool hasAttackCharacter { get { return HasComponent(GameComponentsLookup.AttackCharacter); } }

    public void AddAttackCharacter(int newAttackerEntityId, int newDefenderEntityId) {
        var index = GameComponentsLookup.AttackCharacter;
        var component = CreateComponent<AttackCharacterComponent>(index);
        component.AttackerEntityId = newAttackerEntityId;
        component.DefenderEntityId = newDefenderEntityId;
        AddComponent(index, component);
    }

    public void ReplaceAttackCharacter(int newAttackerEntityId, int newDefenderEntityId) {
        var index = GameComponentsLookup.AttackCharacter;
        var component = CreateComponent<AttackCharacterComponent>(index);
        component.AttackerEntityId = newAttackerEntityId;
        component.DefenderEntityId = newDefenderEntityId;
        ReplaceComponent(index, component);
    }

    public void RemoveAttackCharacter() {
        RemoveComponent(GameComponentsLookup.AttackCharacter);
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

    static Entitas.IMatcher<GameEntity> _matcherAttackCharacter;

    public static Entitas.IMatcher<GameEntity> AttackCharacter {
        get {
            if (_matcherAttackCharacter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AttackCharacter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttackCharacter = matcher;
            }

            return _matcherAttackCharacter;
        }
    }
}
