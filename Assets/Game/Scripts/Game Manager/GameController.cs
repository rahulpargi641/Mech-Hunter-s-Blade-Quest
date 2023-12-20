
public class GameController // more things to add later
{
    public bool IsGameOver { get { return model.GameOver; } set { model.GameOver = value; } }
    public bool IsGameComplete { get { return model.GameComplete; } set { model.GameComplete = value; } }

    private GameModel model;

    public GameController(GameModel model)
    {
        this.model = model;
    }
}
