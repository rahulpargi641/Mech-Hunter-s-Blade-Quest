using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    [SerializeField] GameObject UI_Pause;
    [SerializeField] GameObject UI_GameOver;
    [SerializeField] GameObject UI_GameFinished;

    [SerializeField] string mainMenuName = "MainMenu";
    [SerializeField] private int loadDelay = 5;

    private EGameUIState currentState;

    private void Start()
    {
        SwitchUIState(EGameUIState.GamePlay);

        EventService.Instance.onPlayerDeath += ShowGameOverUI;
        EventService.Instance.onAllEnemiesDead += ShowGameCompletedUI;
    }

    private void OnDestroy()
    {
        EventService.Instance.onPlayerDeath -= ShowGameOverUI;
        EventService.Instance.onAllEnemiesDead -= ShowGameCompletedUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseState();
    }

    public void TogglePauseState()
    {
        SwitchUIState(currentState == EGameUIState.GamePlay ? EGameUIState.Pause : EGameUIState.GamePlay);
    }

    public void Button_MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuName);
    }

    public void Button_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOverUI()
    {
        ShowUIAfterDelay(EGameUIState.GameOver);
    }

    public void ShowGameCompletedUI()
    {
        ShowUIAfterDelay(EGameUIState.GameCompleted);
    }

    public void SwitchUIState(EGameUIState state)
    {
        DeactivateAllUIElements();
        HandleTimeScale(state);
        ActivateUIElement(state);

        currentState = state;
    }

    private void DeactivateAllUIElements()
    {
        UI_Pause.SetActive(false);
        UI_GameFinished.SetActive(false);
        UI_GameOver.SetActive(false);
    }

    private void HandleTimeScale(EGameUIState state)
    {
        Time.timeScale = state == EGameUIState.Pause ? 0 : 1;
    }

    private void ActivateUIElement(EGameUIState state)
    {
        switch (state)
        {
            case EGameUIState.Pause:
                UI_Pause.SetActive(true);
                break;
            case EGameUIState.GameCompleted:
                UI_GameFinished.SetActive(true);
                break;
            case EGameUIState.GameOver:
                UI_GameOver.SetActive(true);
                break;
        }
    }

    private void ShowUIAfterDelay(EGameUIState state)
    {
        StartCoroutine(WaitForDelay(state));
    }

    private IEnumerator WaitForDelay(EGameUIState state)
    {
        yield return new WaitForSecondsRealtime(loadDelay);

        SwitchUIState(state);
    }
}
