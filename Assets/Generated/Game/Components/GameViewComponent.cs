//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SemoGames.PTG.View.ViewComponent view { get { return (SemoGames.PTG.View.ViewComponent)GetComponent(GameComponentsLookup.View); } }
    public bool hasView { get { return HasComponent(GameComponentsLookup.View); } }

    public void AddView(UnityEngine.GameObject newView) {
        var component = CreateComponent<SemoGames.PTG.View.ViewComponent>(GameComponentsLookup.View);
        component.View = newView;
        AddComponent(GameComponentsLookup.View, component);
    }

    public void ReplaceView(UnityEngine.GameObject newView) {
        var component = CreateComponent<SemoGames.PTG.View.ViewComponent>(GameComponentsLookup.View);
        component.View = newView;
        ReplaceComponent(GameComponentsLookup.View, component);
    }

    public void RemoveView() {
        RemoveComponent(GameComponentsLookup.View);
    }
}
