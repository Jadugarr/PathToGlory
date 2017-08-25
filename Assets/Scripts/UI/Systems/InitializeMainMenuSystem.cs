using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.CreateEntity()
            .AddDisplayUI(AssetTypes.MainMenu, new MainMenuProperties());
    }
}