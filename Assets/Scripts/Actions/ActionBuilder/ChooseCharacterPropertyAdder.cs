using System;
using System.Collections.Generic;
using Entitas;
using Promises;

public class ChooseCharacterPropertyAdder : IActionPropertyAdder
{
    private GameContext context;
    private GameEntity actionEntity;
    private IGroup<GameEntity> enemyGroup;
    private IGroup<GameEntity> choseCharacterGroup;
    private Action successCallback;
    private Action<string> errorCallback;
    private Deferred<GameEntity> chooseCharacterPromise;

    public Deferred<GameEntity> Execute(GameContext context, GameEntity actionEntity, Action successCallback,
        Action<string> errorCallback)
    {
        chooseCharacterPromise = new Deferred<GameEntity>();

        this.actionEntity = actionEntity;
        this.context = context;
        this.successCallback = successCallback;
        this.errorCallback = errorCallback;

        enemyGroup = context.GetGroup(GameMatcher.Enemy);
        choseCharacterGroup = context.GetGroup(GameMatcher.ChoseCharacter);

        DisplayCharacterChooser();
        return chooseCharacterPromise;
    }

    public void Cancel()
    {
        HideCharacterChooser();
    }

    private void DisplayCharacterChooser()
    {
        List<int> enemyIds = new List<int>(enemyGroup.count);

        foreach (GameEntity gameEntity in enemyGroup)
        {
            enemyIds.Add(gameEntity.id.Id);
        }

        GameEntity displayUi = context.CreateEntity();
        displayUi.AddDisplayUI(AssetTypes.CharacterChooser,
            new CharacterChooserProperties(enemyIds.ToArray(), context));

        choseCharacterGroup.OnEntityAdded += OnChoseCharacter;
    }

    private void HideCharacterChooser()
    {
        GameEntity hideUiEntity = context.CreateEntity();
        hideUiEntity.AddHideUi(new[]
        {
            AssetTypes.CharacterChooser
        });

        choseCharacterGroup.OnEntityAdded -= OnChoseCharacter;
    }

    private void OnChoseCharacter(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        choseCharacterGroup.OnEntityAdded -= OnChoseCharacter;
        HideCharacterChooser();
        actionEntity.AddTarget(entity.choseCharacter.ChosenEntityId);

        successCallback();
    }
}