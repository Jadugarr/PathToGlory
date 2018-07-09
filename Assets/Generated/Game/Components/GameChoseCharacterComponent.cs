//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChoseCharacterComponent choseCharacter { get { return (ChoseCharacterComponent)GetComponent(GameComponentsLookup.ChoseCharacter); } }
    public bool hasChoseCharacter { get { return HasComponent(GameComponentsLookup.ChoseCharacter); } }

    public void AddChoseCharacter(int newChosenEntityId) {
        var index = GameComponentsLookup.ChoseCharacter;
        var component = CreateComponent<ChoseCharacterComponent>(index);
        component.ChosenEntityId = newChosenEntityId;
        AddComponent(index, component);
    }

    public void ReplaceChoseCharacter(int newChosenEntityId) {
        var index = GameComponentsLookup.ChoseCharacter;
        var component = CreateComponent<ChoseCharacterComponent>(index);
        component.ChosenEntityId = newChosenEntityId;
        ReplaceComponent(index, component);
    }

    public void RemoveChoseCharacter() {
        RemoveComponent(GameComponentsLookup.ChoseCharacter);
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

    static Entitas.IMatcher<GameEntity> _matcherChoseCharacter;

    public static Entitas.IMatcher<GameEntity> ChoseCharacter {
        get {
            if (_matcherChoseCharacter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChoseCharacter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChoseCharacter = matcher;
            }

            return _matcherChoseCharacter;
        }
    }
}
