namespace Entitas.Extensions
{
#pragma warning disable 618
    public static class GameContextExtensions
    {
        public static void SetNewSubstate(this GameContext context, SubState newSubstate)
        {
            context.ReplaceSubState(context.subState.CurrentSubState, newSubstate);
        }

        public static void SetNewGamestate(this GameContext context, GameState newGameState)
        {
            context.ReplaceGameState(context.gameState.CurrentGameState, newGameState);
        }
    }
#pragma warning restore 618
}