using System.Collections.Generic;
using Entitas;
using UnityEngine;

public static class GameSystemService
{
    private static List<Systems> activeSystems = new List<Systems>();
    private static List<Systems> systemsToAdd;
    private static List<Systems> systemsToRemove;
    private static Dictionary<GameState, Systems> stateSystemMap = new Dictionary<GameState, Systems>();
    // TODO: Create substate system
    private static Dictionary<SubState, Systems> subStateSystemMap = new Dictionary<SubState, Systems>();

    public static void AddActiveSystems(Systems systems)
    {
        if (systemsToAdd == null)
        {
            systemsToAdd = new List<Systems>();
        }

        if (!systemsToAdd.Contains(systems))
        {
            systemsToAdd.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried adding the same systems multiple times!");
        }
    }

    public static void RemoveActiveSystems(Systems systems)
    {
        if (systemsToRemove == null)
        {
            systemsToRemove = new List<Systems>();
        }

        if (!systemsToRemove.Contains(systems))
        {
            systemsToRemove.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried removing systems although they're not even active!");
        }
    }

    public static void RefreshActiveSystems()
    {
        if (systemsToAdd != null)
        {
            foreach (Systems systems in systemsToAdd)
            {
                activeSystems.Add(systems);
            }

            systemsToAdd.Clear();
            systemsToAdd = null;
        }

        if (systemsToRemove != null)
        {
            foreach (Systems systems in systemsToRemove)
            {
                activeSystems.Remove(systems);
            }

            systemsToRemove.Clear();
            systemsToRemove = null;
        }
    }

    public static List<Systems> GetActiveSystems()
    {
        return activeSystems;
    }

    public static void AddSystemMapping(GameState state, Systems systems)
    {
        if (!stateSystemMap.ContainsKey(state))
        {
            stateSystemMap.Add(state, systems);
        }
        else
        {
            Debug.LogWarning("System map already contains systems for GameState: " + state);
        }
    }

    public static Systems GetSystemMapping(GameState state)
    {
        Systems returnValue;

        if (stateSystemMap.TryGetValue(state, out returnValue))
        {
            return returnValue;
        }
        else
        {
            return null;
        }
    }

    public static bool HasSystemMapping(GameState state)
    {
        return stateSystemMap.ContainsKey(state);
    }
}