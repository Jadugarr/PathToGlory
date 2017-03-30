using Entitas;

namespace SemoGames.PTG.Battle
{
    [Game]
    public class TimeUntilActionComponent : IComponent
    {
        public float RemainingTime;
        public float TotalTime;
    }
}
