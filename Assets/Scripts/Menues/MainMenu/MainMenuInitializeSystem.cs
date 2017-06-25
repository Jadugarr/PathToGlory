using Entitas;
public class MainMenuInitializeSystem : IInitializeSystem {
    
    public void Initialize()
    {
        Contexts.sharedInstance.game.CreateEntity();
        //.AddUI();
    }
}
