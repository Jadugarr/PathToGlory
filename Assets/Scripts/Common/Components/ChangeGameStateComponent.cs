using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class ChangeGameStateComponent : IComponent
{
    public GameState NewGameState;
}