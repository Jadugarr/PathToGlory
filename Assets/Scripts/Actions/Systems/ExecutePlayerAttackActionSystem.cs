using System;
using System.Collections.Generic;
using Entitas;

public class ExecutePlayerAttackActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> readyToExecuteGroup;
    private IGroup<GameEntity> enemyGroup;
    private IGroup<GameEntity> characterChosenGroup;

    private Queue<GameEntity> attackQueue = new Queue<GameEntity>();
    private bool isExecuting;

    public ExecutePlayerAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
        readyToExecuteGroup = context.GetGroup(GameMatcher.ExecuteAction);
        enemyGroup = context.GetGroup(GameMatcher.Enemy);
        characterChosenGroup = context.GetGroup(GameMatcher.ChoseCharacter);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChoseAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity actionEntity =
            context.GetEntityWithId(readyToExecuteGroup.GetSingleEntity().executeAction.ActionId);
        GameEntity executionerEntity = context.GetEntityWithId(actionEntity.battleAction.EntityId);
        return entity.battleAction.ActionType == ActionType.AttackCharacter && executionerEntity.isPlayer;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            attackQueue.Enqueue(gameEntity);
        }

        if (isExecuting == false)
        {
            isExecuting = true;
            ProcessQueue();
        }
    }

    private void ProcessQueue()
    {
        GameEntity choseActionEntity = attackQueue.Dequeue();
        List<int> enemyIds = new List<int>(3);
        foreach (GameEntity gameEntity in enemyGroup.GetEntities())
        {
            enemyIds.Add(gameEntity.id.Id);
        }
        CharacterChooserProperties props =
            new CharacterChooserProperties(choseActionEntity.choseAction.EntityId,
                enemyIds.ToArray(), context);
        GameEntity displayCharacterChooserEntity = context.CreateEntity();
        displayCharacterChooserEntity.AddDisplayUI(AssetTypes.CharacterChooser, props);

        characterChosenGroup.OnEntityAdded += OnCharacterChosen;
    }

    private void OnCharacterChosen(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        characterChosenGroup.OnEntityAdded -= OnCharacterChosen;

        GameEntity attackEntity = context.CreateEntity();
        attackEntity.AddAttackCharacter(entity.choseCharacter.ChoosingEntityId, entity.choseCharacter.ChosenEntityId);

        if (attackQueue.Count > 0)
        {
            ProcessQueue();
        }
        else
        {
            isExecuting = false;
        }
    }
}