//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public HideUiComponent hideUi { get { return (HideUiComponent)GetComponent(GameComponentsLookup.HideUi); } }
    public bool hasHideUi { get { return HasComponent(GameComponentsLookup.HideUi); } }

    public void AddHideUi(string[] newAssetName) {
        var index = GameComponentsLookup.HideUi;
        var component = CreateComponent<HideUiComponent>(index);
        component.AssetName = newAssetName;
        AddComponent(index, component);
    }

    public void ReplaceHideUi(string[] newAssetName) {
        var index = GameComponentsLookup.HideUi;
        var component = CreateComponent<HideUiComponent>(index);
        component.AssetName = newAssetName;
        ReplaceComponent(index, component);
    }

    public void RemoveHideUi() {
        RemoveComponent(GameComponentsLookup.HideUi);
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

    static Entitas.IMatcher<GameEntity> _matcherHideUi;

    public static Entitas.IMatcher<GameEntity> HideUi {
        get {
            if (_matcherHideUi == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HideUi);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHideUi = matcher;
            }

            return _matcherHideUi;
        }
    }
}