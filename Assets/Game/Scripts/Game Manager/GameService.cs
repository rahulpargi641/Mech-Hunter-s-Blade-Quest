using System.Collections;
using UnityEngine;

public class GameService : MonoSingletonGeneric<GameService>
{
    private GameController controller;
    private int waitDuration = 5;

    void Start()
    {
        GameModel model = new GameModel();
        controller = new GameController(model);

        EventService.Instance.onPlayerDeathAction += GameOver;
        EventService.Instance.onAllEnemiesDeadAction += GameFinished;
    }

    private void OnDestroy()
    {
        EventService.Instance.onPlayerDeathAction -= GameOver;
        EventService.Instance.onAllEnemiesDeadAction -= GameFinished;
    }

    void Update()
    {
        if(controller.IsGameOver())
        {
            StartCoroutine(ShowGameOverUI());
        }

        if(controller.IsGameFinished())
        {
            StartCoroutine(ShowGameFinishedUI());
        }
    }

    IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(waitDuration);

        GameUIService.Instance.ShowGameOverUI();
    }

    IEnumerator ShowGameFinishedUI()
    {
        yield return new WaitForSeconds(waitDuration);

        GameUIService.Instance.ShowGameFinishedUI();
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
