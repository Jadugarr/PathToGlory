using System;
using System.Collections.Generic;
using Entitas;

public class ExecutePlayerAttackActionSystem : ReactiveSystem<GameEntity>
{
    private GameContext context;
    private IGroup<GameEntity> readyToActGroup;
    private IGroup<GameEntity> enemyGroup;
    private IGroup<GameEntity> characterChosenGroup;

    public ExecutePlayerAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
        this.context = (GameContext) context;
        readyToActGroup = context.GetGroup(GameMatcher.ReadyToAct);
        enemyGroup = context.GetGroup(GameMatcher.Enemy);
        characterChosenGroup = context.GetGroup(GameMatcher.ChoseCharacter);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChoseAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        GameEntity entityReadyToAct =
            context.GetEntityWithId(readyToActGroup.GetSingleEntity().readyToAct.EntityReadyToActId);
        return entity.choseAction.ActionType == ActionType.AttackCharacter && entityReadyToAct.isPlayer;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        List<int> enemyIds = new List<int>(3);
        foreach (GameEntity gameEntity in enemyGroup.GetEntities())
        {
            enemyIds.Add(gameEntity.id.Id);
        }

        CharacterChooserProperties props =
            new CharacterChooserProperties(readyToActGroup.GetSingleEntity().readyToAct.EntityReadyToActId,
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
    }
}