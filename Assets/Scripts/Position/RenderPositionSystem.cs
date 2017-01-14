using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace SemoGames.PTG.Position
{
    public class RenderPositionSystem : ReactiveSystem
    {
        public RenderPositionSystem(Context context) : base(context) {

    }

    protected override Collector GetTrigger(Context context) {
        return context.CreateCollector(Matcher.AllOf(CoreMatcher.Position, CoreMatcher.View));
    }

    protected override bool Filter(Entity entity) {
        // TODO Entitas 0.36.0 Migration
        // ensure was: 
        // exclude was: 

        return true;
    }
        

        protected override void Execute(List<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                PositionComponent pos = e.position;
                e.view.View.transform.position = pos.position;
            }
        }
    }
}
