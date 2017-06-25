using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private Button battlePrototypeButton;

    [SerializeField] private Button exitGameButton;

    private GameContext context;

    private void Awake()
    {
        context = Contexts.sharedInstance.game;
        AddEventListeners();
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name != GameSceneConstants.MainMenuScene)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        RemoveEventListeners();
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