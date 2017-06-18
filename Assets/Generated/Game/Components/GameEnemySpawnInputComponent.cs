//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly EnemySpawnInputComponent enemySpawnInputComponent = new EnemySpawnInputComponent();

    public bool isEnemySpawnInput {
        get { return HasComponent(GameComponentsLookup.EnemySpawnInput); }
        set {
            if(value != isEnemySpawnInput) {
                if(value) {
                    AddComponent(GameComponentsLookup.EnemySpawnInput, enemySpawnInputComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.EnemySpawnInput);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherEnemySpawnInput;

    public static Entitas.IMatcher<GameEntity> EnemySpawnInput {
        get {
            if(_matcherEnemySpawnInput == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EnemySpawnInput);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEnemySpawnInput = matcher;
            }

            return _matcherEnemySpawnInput;
        }
    }
}