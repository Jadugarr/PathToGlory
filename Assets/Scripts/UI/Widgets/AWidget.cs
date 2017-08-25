using UnityEngine;

public abstract class AWidget : MonoBehaviour
{
    protected IWidgetProperties properties;

    public abstract void Open();

    public abstract void Close();

    public abstract string GetName();

    public abstract UiComponentType GetComponentType();

    public void ApplyProperties(IWidgetProperties newProperties)
    {
        properties = newProperties;
    }

    protected virtual void OnNewProperties()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    protected virtual void OnShow()
    {
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnHide()
    {
    }
}