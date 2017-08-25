using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMainMenuBehaviour : AWidget
{
    private Button returnButton;
    private GameContext context;

    public override void Open()
    {
        returnButton = GetComponent<Button>();
        context = Contexts.sharedInstance.game;
        AddEventListener();
    }

    public override void Close()
    {
        RemoveEventListener();
    }

    public override string GetName()
    {
        return AssetTypes.ReturnButton;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    // TODO: I will most likely have to create components and systems that signal and handle scene changes and logic specific to this button

    private void OnButtonClicked()
    {
        context.CreateEntity()
            .AddChangeScene(GameSceneConstants.MainMenuScene);
    }

    private void AddEventListener()
    {
        returnButton.onClick.AddListener(OnButtonClicked);
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void RemoveEventListener()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
        returnButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnSceneChanged(Scene previouScene, Scene newScene)
    {
        GameEntity sceneChangedEntity = context.CreateEntity();
        sceneChangedEntity.AddSceneChanged(newScene.name);
    }
}