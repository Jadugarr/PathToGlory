//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity changeGameStateEntity { get { return GetGroup(GameMatcher.ChangeGameState).GetSingleEntity(); } }
    public ChangeGameStateComponent changeGameState { get { return changeGameStateEntity.changeGameState; } }
    public bool hasChangeGameState { get { return changeGameStateEntity != null; } }

    public GameEntity SetChangeGameState(GameState newNewGameState) {
        if (hasChangeGameState) {
            throw new Entitas.EntitasException("Could not set ChangeGameState!\n" + this + " already has an entity with ChangeGameStateComponent!",
                "You should check if the context already has a changeGameStateEntity before setting it or use context.ReplaceChangeGameState().");
        }
        var entity = CreateEntity();
        entity.AddChangeGameState(newNewGameState);
        return entity;
    }

    public void ReplaceChangeGameState(GameState newNewGameState) {
        var entity = changeGameStateEntity;
        if (entity == null) {
            entity = SetChangeGameState(newNewGameState);
        } else {
            entity.ReplaceChangeGameState(newNewGameState);
        }
    }

    public void RemoveChangeGameState() {
        changeGameStateEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChangeGameStateComponent changeGameState { get { return (ChangeGameStateComponent)GetComponent(GameComponentsLookup.ChangeGameState); } }
    public bool hasChangeGameState { get { return HasComponent(GameComponentsLookup.ChangeGameState); } }

    public void AddChangeGameState(GameState newNewGameState) {
        var index = GameComponentsLookup.ChangeGameState;
        var component = (ChangeGameStateComponent)CreateComponent(index, typeof(ChangeGameStateComponent));
        component.NewGameState = newNewGameState;
        AddComponent(index, component);
    }

    public void ReplaceChangeGameState(GameState newNewGameState) {
        var index = GameComponentsLookup.ChangeGameState;
        var component = (ChangeGameStateComponent)CreateComponent(index, typeof(ChangeGameStateComponent));
        component.NewGameState = newNewGameState;
        ReplaceComponent(index, component);
    }

    public void RemoveChangeGameState() {
        RemoveComponent(GameComponentsLookup.ChangeGameState);
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

    static Entitas.IMatcher<GameEntity> _matcherChangeGameState;

    public static Entitas.IMatcher<GameEntity> ChangeGameState {
        get {
            if (_matcherChangeGameState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChangeGameState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChangeGameState = matcher;
            }

            return _matcherChangeGameState;
        }
    }
}