using System;

public abstract class ABattleAction<T> where T : IBattleActionProperties
{
    public event Action ActionFinished;

    protected T properties;

    public void SetProperties(T props)
    {
        properties = props;
    }

    public abstract void Execute();

    protected virtual void NewPropertiesSet()
    {
    }

    protected void Finish()
    {
        if (ActionFinished != null) ActionFinished();
    }
}