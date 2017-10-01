using Entitas;
using UnityEngine;

public class InitializeBattleSystem : IInitializeSystem
{
    private GameContext context;

    public InitializeBattleSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        CreatePlayerEntities();
        CreateEnemyEntities();
        CreateReturnButton();
    }

    private void CreateReturnButton()
    {
        context.CreateEntity()
            .AddDisplayUI(AssetTypes.ReturnButton, new ReturnButtonProperties());
    }

    private void CreatePlayerEntities()
    {
        GameObject[] playerCharacters = GameObject.FindGameObjectsWithTag(Tags.Player);

        foreach (GameObject playerCharacter in playerCharacters)
        {
            GameEntity entity = context.CreateEntity();
            entity.isPlayer = true;
            entity.AddView(playerCharacter);
            entity.AddPosition(playerCharacter.transform.position);
            entity.AddHealth(100);
            entity.AddAttack(10);
            entity.AddDefense(5);
            entity.AddSpeed(10);
            entity.AddCharacterBattleState(CharacterBattleState.WaitingToChoose);
            entity.AddTimeUntilAction(10f, 10f);
            SpriteRenderer spriteRenderer = playerCharacter.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                entity.AddBattleImage(spriteRenderer.sprite);
            }

            entity.isBattle = true;
        }
    }

    private void CreateEnemyEntities()
    {
        GameObject[] enemyCharacters = GameObject.FindGameObjectsWithTag(Tags.Enemy);

        foreach (GameObject enemyCharacter in enemyCharacters)
        {
            GameEntity entity = context.CreateEntity();
            entity.isEnemy = true;
            entity.AddView(enemyCharacter);
            entity.AddPosition(enemyCharacter.transform.position);
            entity.AddHealth(10);
            entity.AddAttack(10);
            entity.AddDefense(5);
            entity.AddSpeed(5);
            entity.AddCharacterBattleState(CharacterBattleState.WaitingToChoose);
            entity.AddTimeUntilAction(10f, 10f);
            SpriteRenderer spriteRenderer = enemyCharacter.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                entity.AddBattleImage(spriteRenderer.sprite);
            }

            entity.isBattle = true;
        }
    }
}