using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController controller;

    private GameContext context;
    private string activeSceneName = "";

    public void Awake()
    {
        if (controller == null)
        {
            //DontDestroyOnLoad(gameObject);
            controller = this;
            context = Contexts.sharedInstance.game;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene newScene, LoadSceneMode loadMode)
    {
        GameEntity newEntity = context.CreateEntity();
        newEntity.AddSceneChanged(activeSceneName, newScene.name);
        activeSceneName = newScene.name;
    }
}