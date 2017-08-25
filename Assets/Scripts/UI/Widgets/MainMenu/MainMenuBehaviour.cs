using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : AWidget
{
    [SerializeField] private Button battlePrototypeButton;

    [SerializeField] private Button exitGameButton;

    private GameContext context;

    public override void Open()
    {
        context = Contexts.sharedInstance.game;
        AddEventListeners();
    }

    public override void Close()
    {
        RemoveEventListeners();
    }

    public override string GetName()
    {
        return AssetTypes.MainMenu;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        if (newScene.name != GameSceneConstants.MainMenuScene)
        {
            context.ReplaceGameState(GameState.Battle);
            Destroy(gameObject);
        }
    }

    private void OnBattleButtonClicked()
    {
        context.CreateEntity()
            .AddChangeScene(GameSceneConstants.BattleScene);
    }

    private void OnExitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void AddEventListeners()
    {
        battlePrototypeButton.onClick.AddListener(OnBattleButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void RemoveEventListeners()
    {
        battlePrototypeButton.onClick.RemoveListener(OnBattleButtonClicked);
        exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}