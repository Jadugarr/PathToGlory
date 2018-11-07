using System.Collections.Generic;
using Entitas.Actions;

namespace Entitas.Utils
{
    public static class BattleActionUtils
    {
        private static Dictionary<ActionType, TargetType> actionTargetsDefinition =
            new Dictionary<ActionType, TargetType>
            {
                {ActionType.None, TargetType.None},
                {ActionType.Defend, TargetType.Self},
                {ActionType.AttackCharacter, TargetType.Enemies | TargetType.Allies}
            };

        public static int[] GetTargetEntitiesByActionType(ActionType actionType, GameEntity choosingEntity,
            GameContext context)
        {
            List<int> targetEntityIds = new List<int>();
            TargetType actionTargetType;

            if (actionTargetsDefinition.TryGetValue(actionType, out actionTargetType))
            {
                if (actionTargetType.HasFlag(TargetType.Self))
                {
                    targetEntityIds.Add(choosingEntity.id.Id);
                }

                if (actionTargetType.HasFlag(TargetType.Allies))
                {
                    IGroup<GameEntity> playerEntities =
                        context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Battle));

                    foreach (GameEntity gameEntity in playerEntities.GetEntities())
                    {
                        if (gameEntity.id.Id != choosingEntity.id.Id)
                        {
                            targetEntityIds.Add(gameEntity.id.Id);
                        }
                    }
                }

                if (actionTargetType.HasFlag(TargetType.Enemies))
                {
                    IGroup<GameEntity> enemyEntities =
                        context.GetGroup(GameMatcher.AllOf(GameMatcher.Enemy, GameMatcher.Battle));

                    foreach (GameEntity gameEntity in enemyEntities.GetEntities())
                    {
                        targetEntityIds.Add(gameEntity.id.Id);
                    }
                }
            }

            return targetEntityIds.ToArray();
        }
    }
}