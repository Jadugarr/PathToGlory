using Entitas;
using SemoGames.PTG.Enemy;
using SemoGames.PTG.GameInput;
using SemoGames.PTG.Position;
using System;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
	private Systems systems;
	
	// Use this for initialization
	void Start ()
	{
		Contexts pools = Contexts.sharedInstance;
		pools.SetAllContexts();

		systems = CreateSystems(pools);
		systems.Initialize();
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
