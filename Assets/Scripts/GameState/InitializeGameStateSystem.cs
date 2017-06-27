using Entitas;

public class InitializeGameStateSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.CreateEntity()
            .AddGameState(GameState.MainMenu);
    }
}