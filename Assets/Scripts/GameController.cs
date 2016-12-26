using Entitas;
using SemoGames.PTG.Enemy;
using SemoGames.PTG.GameInput;
using SemoGames.PTG.Position;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private Systems systems;
	
	// Use this for initialization
	void Start ()
	{
		Pools pools = Pools.sharedInstance;
		pools.SetAllPools();

		systems = CreateSystems(pools);
		systems.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
	{
		systems.Execute();
		systems.Cleanup();
	}

	private Systems CreateSystems(Pools pools)
	{
		return new Feature("Systems")
			//Input
			.Add(pools.CreateSystem(new InputSystem()))
			.Add(pools.input.CreateSystem(new ProcessEnemySpawnInputSystem()))
			//Enemy
			.Add(pools.enemy.CreateSystem(new EnemySpawnCooldownSystem()))
			//Position
			.Add(pools.CreateSystem(new RenderPositionSystem()));
	}
}
