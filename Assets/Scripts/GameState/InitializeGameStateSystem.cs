using Entitas;

public class InitializeGameStateSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.ReplaceGameState(GameState.Undefined, GameState.MainMenu);
        Contexts.sharedInstance.game.ReplaceSubState(SubState.Undefined, SubState.Undefined);
    }
}