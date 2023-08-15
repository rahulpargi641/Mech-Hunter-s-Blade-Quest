using System;
using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] PlayerView playerPrefab;
    private PlayerController playerController;

    void Awake()
    {
        PlayerModel playerModel = new();
        //PlayerView playerView = Instantiate(playerPrefab);
        playerController = new PlayerController(playerModel, playerPrefab);
    }
}
