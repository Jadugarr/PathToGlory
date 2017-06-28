using Entitas;
using UnityEngine;

public class InitializeMainMenuSystem : IInitializeSystem {
    
    public void Initialize()
    {
        Contexts.sharedInstance.game.CreateEntity()
        .AddDisplayUI(Resources.Load("MainMenu") as GameObject);
    }
}
