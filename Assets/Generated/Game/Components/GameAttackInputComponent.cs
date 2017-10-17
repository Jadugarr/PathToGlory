//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly AttackInputComponent attackInputComponent = new AttackInputComponent();

    public bool isAttackInput {
        get { return HasComponent(GameComponentsLookup.AttackInput); }
        set {
            if (value != isAttackInput) {
                if (value) {
                    AddComponent(GameComponentsLookup.AttackInput, attackInputComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.AttackInput);
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

    static Entitas.IMatcher<GameEntity> _matcherAttackInput;

    public static Entitas.IMatcher<GameEntity> AttackInput {
        get {
            if (_matcherAttackInput == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AttackInput);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttackInput = matcher;
            }

            return _matcherAttackInput;
        }
    }
}