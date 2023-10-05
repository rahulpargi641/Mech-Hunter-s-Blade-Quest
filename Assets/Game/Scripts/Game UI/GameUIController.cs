public enum GameUI_State
{
    GamePlay, Pause, GameOver, GameFinished
};

public class GameUIController
{
    private GameUIModel model;
    private GameUIView view;
    public GameUIController(GameUIModel model, GameUIView view)
    {
        this.model = model;
        this.view = view;

        view.Controller = this;
    }

    public void SetCurrentState(GameUI_State state)
    {
        model.CurrentState = state;
    }

    public GameUI_State GetCurrentState()
    {
        return model.CurrentState;
    }

    public void ShowGameOverUI()
    {
        view.ShowGameOverUI();
    }

    public void ShowGameFinishedUI()
    {
        view.ShowGameFinishedUI();
    }
}
