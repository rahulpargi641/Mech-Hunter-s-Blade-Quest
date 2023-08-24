using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    [SerializeField] GameObject UI_Pause;
    [SerializeField] GameObject UI_GameOver;
    [SerializeField] GameObject UI_GameFinished;

    //[SerializeField] Button buttonReStart; 
   
    public GameUIController Controller { private get; set; }

    private void Start()
    {
        SwitchUIState(GameUI_State.GamePlay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseState();
            Debug.Log("Pause UI toggle");
        }
    }


    public void SwitchUIState(GameUI_State state)
    {
        UI_Pause.SetActive(false);
        UI_GameFinished.SetActive(false);
        UI_GameOver.SetActive(false);

        Time.timeScale = 1;

        switch (state)
        {
            case GameUI_State.GamePlay:
                break;
            case GameUI_State.Pause:
                Time.timeScale = 0;
                UI_Pause.SetActive(true);
                break;
            case GameUI_State.GameFinished:
                UI_GameFinished.SetActive(true);
                break;
            case GameUI_State.GameOver:
                UI_GameOver.SetActive(true);
                break;
        }

        Controller.SetCurrentState(state);

    }

    public void TogglePauseState()
    {
        GameUI_State currentState = Controller.GetCurrentState();

        if (currentState == GameUI_State.GamePlay)
            SwitchUIState(GameUI_State.Pause);
        else if (currentState == GameUI_State.Pause)
            SwitchUIState(GameUI_State.GamePlay);
    }

    public void Button_MainMenu()
    {

    }

    public void Button_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
