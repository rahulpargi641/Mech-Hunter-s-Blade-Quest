using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] PlayerView playerPrefab;
    private PlayerController playerController;

    void Start()
    {
        PlayerModel playerModel = new();
        //PlayerView playerView = Instantiate(playerPrefab);
        playerController = new PlayerController(playerModel, playerPrefab);
    }

    public void AddHitImpact(Vector3 attackerPos, float force)
    {
        playerController.AddHitImpact(attackerPos, force);
    }

    public void HitImpactOnPlayer()
    {
        playerController.ProcessHitImpact();
    }
}
