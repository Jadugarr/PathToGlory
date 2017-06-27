using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMainMenuBehaviour : MonoBehaviour
{
    private Button returnButton;

    private void Awake()
    {
        returnButton = GetComponent<Button>();
        returnButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Contexts.sharedInstance.game.CreateEntity()
            .AddChangeScene(GameSceneConstants.MainMenuScene);
    }

    private void OnDestroy()
    {
        returnButton.onClick.RemoveListener(OnButtonClicked);
    }
}
