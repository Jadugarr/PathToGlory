//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.MatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

public sealed partial class GameMatcher {

    static IMatcher<GameEntity> _matcherSemoGamesPTGEnemyEnemySpawnCooldown;

    public static IMatcher<GameEntity> SemoGamesPTGEnemyEnemySpawnCooldown {
        get {
            if(_matcherSemoGamesPTGEnemyEnemySpawnCooldown == null) {
                var matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(GameComponentsLookup.SemoGamesPTGEnemyEnemySpawnCooldown);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSemoGamesPTGEnemyEnemySpawnCooldown = matcher;
            }

            return _matcherSemoGamesPTGEnemyEnemySpawnCooldown;
        }
    }
}
