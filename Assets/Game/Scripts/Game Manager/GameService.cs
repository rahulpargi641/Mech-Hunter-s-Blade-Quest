using UnityEngine;

public class GameService : MonoSingletonGeneric<GameService>
{
    private GameController controller;

    void Start()
    {
        GameModel model = new GameModel();
        controller = new GameController(model);

        EventService.Instance.onPlayerDeath += GameOver;
        EventService.Instance.onAllEnemiesDead += GameCompleted;
    }

    private void OnDestroy()
    {
        EventService.Instance.onPlayerDeath -= GameOver;
        EventService.Instance.onAllEnemiesDead -= GameCompleted;
    }

    public void GameCompleted()
    {
        controller.IsGameComplete = true;
    }

    public void GameOver()
    {
        controller.IsGameOver = true;
    }
}
