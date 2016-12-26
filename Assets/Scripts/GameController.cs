using Entitas;
using SemoGames.PTG.Enemy;
using SemoGames.PTG.GameInput;
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
            .Add(pools.input.CreateSystem(new ProcessEnemySpawnInputSystem()));
    }
}
