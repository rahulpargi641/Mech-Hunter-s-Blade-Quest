using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
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

    public void AddHitImpactForce(Vector3 attackerPos, float force)
    {
        playerController.AddHitImpactForce(attackerPos, force);
    }

    public void ApplyHitImpactForce()
    {
        playerController.ApplyHitImpactForce();
    }
}
