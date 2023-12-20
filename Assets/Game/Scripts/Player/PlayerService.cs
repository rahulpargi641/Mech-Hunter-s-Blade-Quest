using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService> // will be used by GameService to spawn the player
{
    [SerializeField] PlayerView playerView;
    [SerializeField] PlayerSO playerSO;

    private PlayerController playerController;

    private void Start()
    {
        PlayerModel playerModel = new PlayerModel(playerSO);
        //PlayerView playerView = Instantiate(playerPrefab);
        playerController = new PlayerController(playerModel, playerView);
    }
}
