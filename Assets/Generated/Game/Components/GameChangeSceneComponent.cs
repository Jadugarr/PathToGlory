//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChangeSceneComponent changeScene { get { return (ChangeSceneComponent)GetComponent(GameComponentsLookup.ChangeScene); } }
    public bool hasChangeScene { get { return HasComponent(GameComponentsLookup.ChangeScene); } }

    public void AddChangeScene(string newSceneName, UnityEngine.SceneManagement.LoadSceneMode newLoadSceneMode) {
        var index = GameComponentsLookup.ChangeScene;
        var component = CreateComponent<ChangeSceneComponent>(index);
        component.SceneName = newSceneName;
        component.LoadSceneMode = newLoadSceneMode;
        AddComponent(index, component);
    }

    public void ReplaceChangeScene(string newSceneName, UnityEngine.SceneManagement.LoadSceneMode newLoadSceneMode) {
        var index = GameComponentsLookup.ChangeScene;
        var component = CreateComponent<ChangeSceneComponent>(index);
        component.SceneName = newSceneName;
        component.LoadSceneMode = newLoadSceneMode;
        ReplaceComponent(index, component);
    }

    public void RemoveChangeScene() {
        RemoveComponent(GameComponentsLookup.ChangeScene);
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

    static Entitas.IMatcher<GameEntity> _matcherChangeScene;

    public static Entitas.IMatcher<GameEntity> ChangeScene {
        get {
            if (_matcherChangeScene == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChangeScene);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChangeScene = matcher;
            }

            return _matcherChangeScene;
        }
    }
}
