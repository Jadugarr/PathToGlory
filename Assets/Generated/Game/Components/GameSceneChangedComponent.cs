//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SceneChangedComponent sceneChanged { get { return (SceneChangedComponent)GetComponent(GameComponentsLookup.SceneChanged); } }
    public bool hasSceneChanged { get { return HasComponent(GameComponentsLookup.SceneChanged); } }

    public void AddSceneChanged(string newPreviousSceneName, string newNewSceneName) {
        var index = GameComponentsLookup.SceneChanged;
        var component = CreateComponent<SceneChangedComponent>(index);
        component.PreviousSceneName = newPreviousSceneName;
        component.NewSceneName = newNewSceneName;
        AddComponent(index, component);
    }

    public void ReplaceSceneChanged(string newPreviousSceneName, string newNewSceneName) {
        var index = GameComponentsLookup.SceneChanged;
        var component = CreateComponent<SceneChangedComponent>(index);
        component.PreviousSceneName = newPreviousSceneName;
        component.NewSceneName = newNewSceneName;
        ReplaceComponent(index, component);
    }

    public void RemoveSceneChanged() {
        RemoveComponent(GameComponentsLookup.SceneChanged);
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

    static Entitas.IMatcher<GameEntity> _matcherSceneChanged;

    public static Entitas.IMatcher<GameEntity> SceneChanged {
        get {
            if (_matcherSceneChanged == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SceneChanged);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSceneChanged = matcher;
            }

            return _matcherSceneChanged;
        }
    }
}
