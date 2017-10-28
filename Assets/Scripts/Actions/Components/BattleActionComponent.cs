using Entitas;

[Game]
public class BattleActionComponent : IComponent
{
    public int EntityId;
    public ActionType ActionType;
    public ActionATBType ActionAtbType;
    public IActionProperties ActionProperties;
    public float TotalTimeToExecution;
    public float RemainingTimeToExecution;
}