using Entitas;

[Game]
public class TimeUntilActionComponent : IComponent
{
    public int ActionId;
    public float RemainingTime;
    public float TotalTime;
}