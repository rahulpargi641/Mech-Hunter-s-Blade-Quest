using UnityEngine;

public class GameService : MonoSingletonGeneric<GameService>
{
    private GameController controller;

    void Start()
    {
        GameModel model = new GameModel();
        controller = new GameController(model);

        EventService.Instance.onPlayerDeathAction += GameOver;
        EventService.Instance.onAllEnemiesDeadAction += GameFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.IsGameOver())
        {
            Debug.Log("Game Over");
            GameUIService.Instance.ShowGameOverUI();
        }

        if(controller.IsGameFinished())
        {
            Debug.Log("Game Finished");
            GameUIService.Instance.ShowGameFinishedUI();
        }
    }

    private void OnDisable()
    {
        EventService.Instance.onPlayerDeathAction -= GameOver;

        EventService.Instance.onAllEnemiesDeadAction += GameFinished;
    }

    public void GameFinished()
    {
        controller.GameFinished();
    }
    public void GameOver()
    {
        controller.GameOver();
    }
}
