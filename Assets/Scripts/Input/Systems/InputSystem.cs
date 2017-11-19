using Configurations;
using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem, ICleanupSystem
{
    private GameContext context;
    private IGroup<GameEntity> inputComponents;

    public InputSystem(GameContext context)
    {
        this.context = context;
        inputComponents = this.context.GetGroup(GameMatcher.Input);
    }

    public void Execute()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (string currentAxis in InputAxis.AxisList)
            {
                float axisValue = Input.GetAxis(currentAxis);

                if (axisValue != 0)
                {
                    InputCommand commandToExecute =
                        InputConfiguration.GetCommandByAxisName(currentAxis);
                    if (commandToExecute != InputCommand.Undefined)
                    {
                        GameEntity inputEntity = context.CreateEntity();
                        inputEntity.AddInput(commandToExecute, axisValue);
                    }
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in inputComponents.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}