using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMainMenuBehaviour : MonoBehaviour
{
    private Button returnButton;
    private GameContext context;

    private void Awake()
    {
        returnButton = GetComponent<Button>();
        context = Contexts.sharedInstance.game;
        AddEventListener();
    }

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
        if (newScene.name == GameSceneConstants.MainMenuScene)
        {
            context.ReplaceGameState(GameState.MainMenu);
        }
    }

    private void OnDestroy()
    {
        RemoveEventListener();
    }
}