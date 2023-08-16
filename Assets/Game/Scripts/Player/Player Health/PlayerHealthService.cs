using UnityEngine;

public class PlayerHealthService : MonoSingletonGeneric<PlayerHealthService>
{
    [SerializeField] PlayerHealthView playerHealthView;
    private PlayerHealthController playerHealthController;

    void Start()
    {
        HealthModel healthModel = new HealthModel();
        playerHealthController = new PlayerHealthController(healthModel, playerHealthView);
    }

    public void ApplyDamage(int damage)
    {
        playerHealthController.ApplyDamage(damage);
    }

    public void AddHealth(int healthPoints)
    {
        playerHealthController.AddHealth(healthPoints);
    }

    internal void InvokePlayerDeath()
    {
        EventService.Instance.InvokePlayerDeathAction();
    }
}
