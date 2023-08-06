using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] PlayerView playerPrefab;
    private PlayerController playerController;

    void Awake()
    {
        PlayerModel playerModel = new();
        PlayerView playerView = Instantiate(playerPrefab);
        playerController = new PlayerController(playerModel, playerView);
    }
}
