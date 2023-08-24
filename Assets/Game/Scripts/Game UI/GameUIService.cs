using UnityEngine;

public class GameUIService : MonoSingletonGeneric<GameUIService>
{
    [SerializeField] GameUIView gameUIView;
    private GameUIController gameUIController;

    // Start is called before the first frame update
    void Start()
    {
        GameUIModel model = new GameUIModel();
        gameUIController = new GameUIController(model, gameUIView);
    }
}
