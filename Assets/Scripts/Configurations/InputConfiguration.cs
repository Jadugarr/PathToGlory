using System.Collections.Generic;
using UnityEngine;

public static class InputConfiguration
{
    // "string" is the name of the defined axis
    private static Dictionary<GameState, Dictionary<string, InputCommand>> InputMaps;

    private static Dictionary<string, InputCommand> activeInputMap;

    static InputConfiguration()
    {
        InputMaps = new Dictionary<GameState, Dictionary<string, InputCommand>>()
        {
            {
                GameState.Battle, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Cancel, InputCommand.CancelAction}
                }
            },
            {
                GameState.MainMenu, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Cancel, InputCommand.ExitMenu}
                }
            },
            {
                GameState.Paused, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Cancel, InputCommand.Unpause}
                }
            }
        };
    }

    public static InputCommand GetCommandByAxisName(string axisName)
    {
        if (activeInputMap.ContainsKey(axisName))
        {
            return activeInputMap[axisName];
        }

        Debug.LogWarning("No active input found for axis: " + axisName);
        return InputCommand.Undefined;
    }

    public static void ChangeActiveInputMap(GameState state)
    {
        Dictionary<string, InputCommand> newMap;

        if (InputMaps.TryGetValue(state, out newMap))
        {
            activeInputMap = newMap;
            Debug.Log("Switched active input map for state: " + state);
        }
        else
        {
            Debug.LogWarning("No input map defined for game state: " + state);
        }
    }
}