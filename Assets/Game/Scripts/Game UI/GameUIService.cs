using UnityEngine;

public class GameUIService : MonoSingletonGeneric<GameUIService>
{
    [SerializeField] GameUIView gameUIView;
    private GameUIController gameUIController;

    void Start()
    {
        GameUIModel model = new GameUIModel();
        gameUIController = new GameUIController(model, gameUIView);
    }

    public void ShowGameOverUI()
    {
        gameUIController.ShowGameOverUI();
    }

    public void ShowGameFinishedUI()
    {
        gameUIController.ShowGameFinishedUI();
    }
}
