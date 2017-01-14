using Entitas;
using SemoGames.PTG.Configurations;
using SemoGames.PTG.Enemy;
using SemoGames.PTG.GameInput;
using SemoGames.PTG.Position;
using System;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField]
    private SpawnConfiguration spawnConfiguration;

    [SerializeField]
    private CharacterConfiguration characterConfiguration;

	private Systems systems;
	
	// Use this for initialization
	void Start ()
	{
        InitConfigs();

		Contexts pools = Contexts.sharedInstance;
		pools.SetAllContexts();

		systems = CreateSystems(pools);
		systems.Initialize();
	}

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
    }
	
	// Update is called once per frame
	void Update ()
	{
		systems.Execute();
		systems.Cleanup();
	}

	private Systems CreateSystems(Contexts pools)
	{
		return new Feature("Systems")
			//Input
			.Add((new InputSystem(pools.core)))
			.Add((new ProcessEnemySpawnInputSystem(pools.core)))
			//Enemy
			.Add((new EnemySpawnCooldownSystem(pools.core)))
			//Position
			.Add((new RenderPositionSystem(pools.core)));
	}
}
