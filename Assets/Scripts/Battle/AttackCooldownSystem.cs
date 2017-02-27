using Entitas;
using UnityEngine;

namespace SemoGames.PTG.Battle
{
    public class AttackCooldownSystem : IExecuteSystem
    {
        private GameContext context;

        public AttackCooldownSystem(GameContext context)
        {
            this.context = context;
        }

        public void Execute()
        {
            if (context.hasAttackCooldown)
            {
                context.attackCooldown.Cooldown -= Time.deltaTime;

                if (context.attackCooldown.Cooldown <= 0f)
                {
                    context.RemoveAttackCooldown();
                }
            }
        }
    }
}