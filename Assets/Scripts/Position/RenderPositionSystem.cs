using Entitas;
using System.Collections.Generic;
using System;

namespace SemoGames.PTG.Position
{
    public class RenderPositionSystem : IEntityCollectorSystem, ISetPools
    {
        public EntityCollector entityCollector
        {
            get
            {
                return collector;
            }
        }

        private EntityCollector collector;

        public void Execute(List<Entity> entities)
        {
        }

        public void SetPools(Pools pools)
        {
            collector = new[] { pools.enemy }.CreateEntityCollector(CoreMatcher.Position);
        }
    }
}
