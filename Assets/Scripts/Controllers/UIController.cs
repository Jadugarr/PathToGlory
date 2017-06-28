using Entitas;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject uiLayer;

    private static UIController controller;

    private IGroup<GameEntity> uiEntities;

    private void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
            uiEntities = Contexts.sharedInstance.game.GetGroup(GameMatcher.DisplayUI);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (uiEntities.count > 0)
        {
            InstatiateUIComponents();
        }
    }

    private void InstatiateUIComponents()
    {
        foreach (GameEntity gameEntity in uiEntities.GetEntities())
        {
            Instantiate(gameEntity.displayUI.ViewToDisplay, uiLayer.transform);
        }
    }
}