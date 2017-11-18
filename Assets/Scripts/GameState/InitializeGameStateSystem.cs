using Entitas;

public class InitializeGameStateSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.SetGameState(GameState.Undefined, GameState.MainMenu);
    }
}