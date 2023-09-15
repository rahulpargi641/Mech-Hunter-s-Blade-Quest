using System.Collections;
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
            StartCoroutine(ShowGameOverUI());
        }

        if(controller.IsGameFinished())
        {
            Debug.Log("Game Finished");
            StartCoroutine(ShowGameFinishedUI());
        }
    }

    IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(5f);

        GameUIService.Instance.ShowGameOverUI();
    }

    IEnumerator ShowGameFinishedUI()
    {
        yield return new WaitForSeconds(5f);

        GameUIService.Instance.ShowGameFinishedUI();
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
