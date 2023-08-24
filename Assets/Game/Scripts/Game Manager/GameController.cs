
public class GameController
{
    private GameModel model;
    public GameController(GameModel model)
    {
        this.model = model;
    }

    public void GameOver()
    {
        model.GameOver = true;
    }

    public void GameFinished()
    {
        model.GameFinished = true;
    }

    public bool IsGameOver()
    {
        if (model.GameOver)
            return true;
        else
            return false;
    }

    public bool IsGameFinished()
    {
        if (model.GameFinished)
            return true;
        else
            return false;
    }

}
