//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity winConditionEntity { get { return GetGroup(GameMatcher.WinCondition).GetSingleEntity(); } }
    public WinConditionComponent winCondition { get { return winConditionEntity.winCondition; } }
    public bool hasWinCondition { get { return winConditionEntity != null; } }

    public GameEntity SetWinCondition(ConditionModifier newConditionModifier, WinConditionState[] newWinConditions) {
        if (hasWinCondition) {
            throw new Entitas.EntitasException("Could not set WinCondition!\n" + this + " already has an entity with WinConditionComponent!",
                "You should check if the context already has a winConditionEntity before setting it or use context.ReplaceWinCondition().");
        }
        var entity = CreateEntity();
        entity.AddWinCondition(newConditionModifier, newWinConditions);
        return entity;
    }

    public void ReplaceWinCondition(ConditionModifier newConditionModifier, WinConditionState[] newWinConditions) {
        var entity = winConditionEntity;
        if (entity == null) {
            entity = SetWinCondition(newConditionModifier, newWinConditions);
        } else {
            entity.ReplaceWinCondition(newConditionModifier, newWinConditions);
        }
    }

    public void RemoveWinCondition() {
        winConditionEntity.Destroy();
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

    public WinConditionComponent winCondition { get { return (WinConditionComponent)GetComponent(GameComponentsLookup.WinCondition); } }
    public bool hasWinCondition { get { return HasComponent(GameComponentsLookup.WinCondition); } }

    public void AddWinCondition(ConditionModifier newConditionModifier, WinConditionState[] newWinConditions) {
        var index = GameComponentsLookup.WinCondition;
        var component = (WinConditionComponent)CreateComponent(index, typeof(WinConditionComponent));
        component.ConditionModifier = newConditionModifier;
        component.WinConditions = newWinConditions;
        AddComponent(index, component);
    }

    public void ReplaceWinCondition(ConditionModifier newConditionModifier, WinConditionState[] newWinConditions) {
        var index = GameComponentsLookup.WinCondition;
        var component = (WinConditionComponent)CreateComponent(index, typeof(WinConditionComponent));
        component.ConditionModifier = newConditionModifier;
        component.WinConditions = newWinConditions;
        ReplaceComponent(index, component);
    }

    public void RemoveWinCondition() {
        RemoveComponent(GameComponentsLookup.WinCondition);
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

    static Entitas.IMatcher<GameEntity> _matcherWinCondition;

    public static Entitas.IMatcher<GameEntity> WinCondition {
        get {
            if (_matcherWinCondition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WinCondition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWinCondition = matcher;
            }

            return _matcherWinCondition;
        }
    }
}
