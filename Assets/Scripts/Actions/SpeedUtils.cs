public static class SpeedUtils
{
    /// <summary>
    /// Returns how much you can deduct from the remaining action time per second
    /// </summary>
    /// <param name="actionType">Type of action the character is performing</param>
    /// <param name="characterSpeed">Speed of the performing character</param>
    public static float GetActionTimeStep(ActionType actionType, float characterSpeed)
    {
        // TODO: Think of a formula that returns reasonable values
        return 1f;
    }
}