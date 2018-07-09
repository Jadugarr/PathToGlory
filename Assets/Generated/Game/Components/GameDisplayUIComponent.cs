//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DisplayUIComponent displayUI { get { return (DisplayUIComponent)GetComponent(GameComponentsLookup.DisplayUI); } }
    public bool hasDisplayUI { get { return HasComponent(GameComponentsLookup.DisplayUI); } }

    public void AddDisplayUI(string newAssetName, IWidgetProperties newProperties) {
        var index = GameComponentsLookup.DisplayUI;
        var component = CreateComponent<DisplayUIComponent>(index);
        component.AssetName = newAssetName;
        component.properties = newProperties;
        AddComponent(index, component);
    }

    public void ReplaceDisplayUI(string newAssetName, IWidgetProperties newProperties) {
        var index = GameComponentsLookup.DisplayUI;
        var component = CreateComponent<DisplayUIComponent>(index);
        component.AssetName = newAssetName;
        component.properties = newProperties;
        ReplaceComponent(index, component);
    }

    public void RemoveDisplayUI() {
        RemoveComponent(GameComponentsLookup.DisplayUI);
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

    static Entitas.IMatcher<GameEntity> _matcherDisplayUI;

    public static Entitas.IMatcher<GameEntity> DisplayUI {
        get {
            if (_matcherDisplayUI == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DisplayUI);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDisplayUI = matcher;
            }

            return _matcherDisplayUI;
        }
    }
}
