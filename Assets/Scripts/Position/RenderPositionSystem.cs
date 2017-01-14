using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace SemoGames.PTG.Position
{
    public class RenderPositionSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(CoreMatcher.Position, CoreMatcher.View).OnEntityAdded(); } }
        

        public void Execute(List<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                PositionComponent pos = e.position;
                e.view.View.transform.position = pos.position;
            }
        }
    }
}
