using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIService : MonoSingletonGeneric<GameUIService>
{
    private GameUIController Controller;

    // Start is called before the first frame update
    void Start()
    {
        GameUIModel model = new GameUIModel();
        Controller = new GameUIController(model);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
