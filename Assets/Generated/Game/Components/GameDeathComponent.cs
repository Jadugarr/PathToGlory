//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SemoGames.PTG.Battle.DeathComponent death { get { return (SemoGames.PTG.Battle.DeathComponent)GetComponent(GameComponentsLookup.Death); } }
    public bool hasDeath { get { return HasComponent(GameComponentsLookup.Death); } }

    public void AddDeath(GameEntity newDeadCharacter) {
        var component = CreateComponent<SemoGames.PTG.Battle.DeathComponent>(GameComponentsLookup.Death);
        component.DeadCharacter = newDeadCharacter;
        AddComponent(GameComponentsLookup.Death, component);
    }

    public void ReplaceDeath(GameEntity newDeadCharacter) {
        var component = CreateComponent<SemoGames.PTG.Battle.DeathComponent>(GameComponentsLookup.Death);
        component.DeadCharacter = newDeadCharacter;
        ReplaceComponent(GameComponentsLookup.Death, component);
    }

    public void RemoveDeath() {
        RemoveComponent(GameComponentsLookup.Death);
    }
}
