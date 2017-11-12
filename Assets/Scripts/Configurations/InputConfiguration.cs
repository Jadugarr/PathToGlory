using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "InputConfiguration", menuName = "Configurations/InputConfiguration")]
public class InputConfiguration : ScriptableObject
{
    // "string" is the name of the defined axis
    [SerializeField] public Dictionary<GameState, Dictionary<string, InputCommand>> InputMaps;

    private Dictionary<string, InputCommand> activeInputMap;

    public InputConfiguration()
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

    public InputCommand GetCommandByAxisName(string axisName)
    {
        if (activeInputMap.ContainsKey(axisName))
        {
            return activeInputMap[axisName];
        }

        Debug.LogWarning("No active input found for axis: " + axisName);
        return InputCommand.Undefined;
    }

    public void ChangeActiveInputMap(GameState state)
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